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
    public GameConstants gameConstants;
    private float distanceToPlayer;
    private float playerRelativeX;

    private bool fireballAvailable = true;
    private bool meleeAvailable = true;
    private float fireballCooldown = 6f;
    private float meleeCooldown = 3f;
    private float bossHealth;
    private float maxBossHealth = 20f;
    private Vector2 velocity = new Vector2(3, 0);
    private int state=0;
    private bool isAttacking = false;
    private bool isFullySpawned = false;

    private int fireballAmount = 4;

    private enum MovementState {idle, walking, cleaving, throwing, death,spawning}

    private AudioSource audioSource;
    public AudioClip fireAudio;
    public AudioClip attackAudio;
    public AudioClip damageAudio;
    public AudioClip victoryAudio;

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

        audioSource = GetComponent<AudioSource>();

        StartCoroutine(Spawning());

    }

    IEnumerator Spawning()
    {
       yield return new WaitForSeconds(2f);
       isFullySpawned = true;
    }
    
    void FixedUpdate()
    {   if (!isAttacking && isFullySpawned)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossIdle") || anim.GetCurrentAnimatorStateInfo(0).IsName("BossWalk")){
                if (distanceToPlayer>4 || distanceToPlayer<-4)
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

        if (Mathf.Abs(distanceToPlayer)<5 && isFullySpawned)
        {
            if (meleeAvailable)
            {
                cleaveAttack();
                StartCoroutine(meleeCountdown());
            }
        } else if (fireballAvailable && isFullySpawned) 
        {
            StartCoroutine(fireballAttack());
            StartCoroutine(fireballCountdown());
        }
    }

    void cleaveAttack()
    {   

        anim.SetTrigger("startCleave");
        
        
        StartCoroutine(DisableMelee());
    }

     IEnumerator meleeCountdown()
    {   
        meleeAvailable = false;
        yield return new WaitForSeconds(meleeCooldown);
        meleeAvailable = true;
    }
    IEnumerator DisableMelee()
    {   
        //yield return new WaitForSeconds(0.25f);
        m_meleeSensor.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        audioSource.PlayOneShot(attackAudio, 3.0f);
        m_meleeSensor.SetActive(false);
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator fireballAttack()
    {
        audioSource.PlayOneShot(fireAudio, 5.0f);
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

    IEnumerator fireballCountdown()
    {   
        fireballAvailable = false;
        yield return new WaitForSeconds(fireballCooldown);
        fireballAvailable = true;
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

    public void TakeDamage(float damage){
        audioSource.PlayOneShot(damageAudio);
        bossHealth -= damage;
        Debug.Log("Boss took damage");
        if(bossHealth <= 0){
            audioSource.PlayOneShot(victoryAudio);
            Debug.Log("Boss died");
            anim.SetInteger("state",(int)MovementState.death);
        }
    }

    
}
