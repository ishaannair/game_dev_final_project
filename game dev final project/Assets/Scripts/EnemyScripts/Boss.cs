using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private GameObject   m_meleeSensor;
    public  GameObject prefab;
    private float distanceToPlayer;
    private float playerRelativeX;

    private bool attackAvailable = true;
    private int attackCooldown = 6;
    private float bossHealth;
    private float maxBossHealth = 200;
    private Vector2 velocity = new Vector2(1, 0);
    private int state=0;
    private bool isAttacking = false;

    private enum MovementState {idle, walking, cleaving, throwing, death}

    // Start is called before the first frame update
    void Start()
    {       
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bossHealth = maxBossHealth;

        m_meleeSensor = this.transform.Find("MeleeSensor").gameObject;
        m_meleeSensor.SetActive(false);
    }

    void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossIdle") || anim.GetCurrentAnimatorStateInfo(0).IsName("BossWalk")){
            if (distanceToPlayer>4 || distanceToPlayer<-4)
            {
                rb.MovePosition(rb.position + Mathf.Sign(distanceToPlayer)*velocity * Time.fixedDeltaTime);
                anim.SetInteger("state",(int)MovementState.walking);
            } else
            {
                anim.SetInteger("state",(int)MovementState.idle);
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {   
        CalculateDistanceToPlayer();

        if (attackAvailable)
        {
            if (Mathf.Abs(distanceToPlayer)<5)
            {   
                cleaveAttack();
                StartCoroutine(attackCountdown());
            } else
            {
                fireballAttack();
                StartCoroutine(attackCountdown());
            }
        }

        //if (Time.timeScale==1.0f && !isAttacking)
        //{   
        //    state = getState();
       //     anim.SetInteger("state", state);
        //}
    }


    void cleaveAttack()
    {   
        isAttacking = true;
        anim.SetTrigger("startCleave");
        StartCoroutine(DisableMelee());
        isAttacking = false;
    }

    void fireballAttack()
    {
        isAttacking = true;
        Debug.Log("Fire");
        anim.SetTrigger("startThrow");
        Instantiate(prefab, transform.position + new Vector3(1.5f*Mathf.Sign(distanceToPlayer),-1.5f,0), Quaternion.identity);
        isAttacking = false;
    }

    IEnumerator attackCountdown()
    {   
        attackAvailable = false;
        yield return new WaitForSeconds(attackCooldown);
        attackAvailable = true;
    }

    private int getState()
    {

        if (rb.velocity.x>.1f)
        {
            sr.flipX = false;
            return (int)MovementState.walking;
        } else if (rb.velocity.x<-.1f)
        {
            sr.flipX = true;
            return (int)MovementState.walking;
        }
        
        return (int)MovementState.idle;
    }


    void CalculateDistanceToPlayer()
    {   
        float xDist = playerObj.transform.position.x - this.transform.position.x;
        float yDist = playerObj.transform.position.y - this.transform.position.y;

        if (playerObj.transform.position.x<this.transform.position.x)
        {
            distanceToPlayer = -Mathf.Sqrt(xDist*xDist + yDist*yDist);
            sr.flipX = false;
            m_meleeSensor.transform.localPosition = new Vector3(0,0,0);
        } else{
            distanceToPlayer =  Mathf.Sqrt(xDist*xDist + yDist*yDist);
            sr.flipX = true;
            m_meleeSensor.transform.localPosition = new Vector3(2f,0,0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Melee")){
            bossHealth-=10f;
        }
    }

    IEnumerator DisableMelee()
    {   
        //yield return new WaitForSeconds(0.25f);
        m_meleeSensor.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        m_meleeSensor.SetActive(false);
    }
}
