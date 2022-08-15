using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossMini : MonoBehaviour
{
    private GameObject playerObj = null;
    public GameConstants gameConstants;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public  GameObject prefab;
    public GameObject boss;
    private float distanceToPlayer;
    private float playerRelativeY;
    public UnityEvent onBossMiniDeath;

    private bool attackAvailable = true;
    private int attackCooldown = 3;
    private float bossHealth;
    private float maxBossHealth = 30f;
    private Vector2 velocity = new Vector2(3, 0);
    private int state=0;
    private bool isAttacking = false;

    private int fireballAmount = 4;

    private enum MovementState {idle, walking, cleaving, throwing, death}

    // Start is called before the first frame update
    void Start()
    {       
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bossHealth = gameConstants.bossMiniHealth;
        gameConstants.bigBossHasSpawned=false;

    }


    void FixedUpdate()
    {   if (!isAttacking)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossIdle") || anim.GetCurrentAnimatorStateInfo(0).IsName("BossWalk")){
                if (Mathf.Abs(distanceToPlayer)>5)
                {
                    Debug.Log(distanceToPlayer);
                    rb.MovePosition(rb.position + Mathf.Sign(distanceToPlayer)*velocity * Time.fixedDeltaTime);
                    anim.SetInteger("state",(int)MovementState.walking);
                } else
                {
                    anim.SetInteger("state",(int)MovementState.idle);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        CalculateDistanceToPlayer();

        if (attackAvailable)
        {
            if (Mathf.Abs(distanceToPlayer)<5 && Mathf.Abs(playerRelativeY)<2.5f)
            {   
                cleaveAttack();
                StartCoroutine(attackCountdown());
            } else
            {
                StartCoroutine(fireballAttack());
                StartCoroutine(attackCountdown());
            }
        }

    }
    void cleaveAttack()
    {   
        isAttacking = true;
        anim.SetTrigger("startCleave");
        StartCoroutine(DisableMelee());
        isAttacking = false;
    }

    IEnumerator fireballAttack()
    {
        isAttacking = true;
        Debug.Log("Fire");
        
        
        Instantiate(prefab, playerObj.transform.position + new Vector3(0,10f,0), Quaternion.identity);
        for (int i = 0 ;  i<fireballAmount; i++)
        {   
            anim.SetTrigger("startThrow"); 
            var position = new Vector3(Random.Range(-10.0f, 10.0f), 10f , 0);
            Instantiate(prefab, playerObj.transform.position + position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1);
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
        playerRelativeY = distanceToPlayer;

        if (playerObj.transform.position.x<this.transform.position.x)
        {
            distanceToPlayer = -Mathf.Sqrt(xDist*xDist + yDist*yDist);
            if( !anim.GetCurrentAnimatorStateInfo(0).IsName("BossCleave")){
                sr.flipX = false;
            }
                
        } else{
            distanceToPlayer =  Mathf.Sqrt(xDist*xDist + yDist*yDist);
            if( !anim.GetCurrentAnimatorStateInfo(0).IsName("BossCleave")){
                sr.flipX = true;
            }
        }
    }

    public void TakeDamage(float damage){
        bossHealth -= damage;
        Debug.Log("Boss took damage");
        if(bossHealth <= 0){
            onBossMiniDeath.Invoke();
            Destroy(this.gameObject);
        }
    }

    IEnumerator DisableMelee()
    {   
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(gameConstants.bossSlashDuration / 2);
        Collider2D[] hitColliders;
        if(sr.flipX){
            hitColliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x + 1f, this.transform.position.y), 0.9f);
        }else{
            hitColliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x - 1f, this.transform.position.y), 0.9f);
        }

        for(int i = 0; i < hitColliders.Length; i++){
            Collider2D col = hitColliders[i];

            if(col.gameObject.CompareTag("Player")){
                col.GetComponent<PlayerController>().EnemyHit(gameConstants.bossMeleeDamage);
                continue;
            }
        }
    }

    
}
