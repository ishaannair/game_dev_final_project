using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;


public class JumperEnemy : MonoBehaviour
{
    public GameConstants gameConstants;
    private GameObject playerObj = null;
    public GameObject[] scraps;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private float distanceToPlayer;
    private float playerRelativeX;
    private float health;
    private int state;

    private bool jumpAvailable = true;
    private bool moveAvailable = true;
    private int jumpCooldown = 5;
    public float velocity = 3f;
    private float maxHealth = 50f;
    private float currentHealth;
    public EnemyVariant variant = EnemyVariant.flesh;
    
    private enum MovementState {idle, running, jumping}

    public AudioClip deathAudio;
    public AudioClip attackAudio;
    public AudioClip damagedAudio;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {       
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        health = gameConstants.jumperHealth;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        CalculateDistanceToPlayer();
        state = getState();
        anim.SetInteger("state", state);

        if (Mathf.Sign(distanceToPlayer)>0){
            sr.flipX = true;
        } else
        {
            sr.flipX = false;
        }

        if (Math.Abs(distanceToPlayer)<10f)
        {
            if (jumpAvailable)
            {

                
                rb.AddForce(new Vector3(5*distanceToPlayer,60,0), ForceMode2D.Impulse);
                jumpAvailable = false;
                StartCoroutine(JumpCountdown());
            }
        } else if (Math.Abs(distanceToPlayer)<gameConstants.enemySightlines)
        {
             rb.velocity = new Vector2(Mathf.Sign(distanceToPlayer) * velocity, rb.velocity.y);
        }

        if (health<=0f)
        {   
            Debug.Log("enemyDied");
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, deathAudio.length);
        }       
    }

    IEnumerator JumpCountdown()
    {
        
        if (GetComponent<SpriteRenderer>().isVisible) {
            audioSource.PlayOneShot(attackAudio, 5.0f);
        }
        yield return new WaitForSeconds(jumpCooldown);
        jumpAvailable = true;
    }

    void CalculateDistanceToPlayer()
    {   
        float xDist = playerObj.transform.position.x - this.transform.position.x;
        float yDist = playerObj.transform.position.y - this.transform.position.y;

        if (playerObj.transform.position.x<this.transform.position.x)
        {
            distanceToPlayer = -Mathf.Sqrt(xDist*xDist + yDist*yDist);
        } else{
            distanceToPlayer =  Mathf.Sqrt(xDist*xDist + yDist*yDist);
        }
    }

    public void TakeDamage(float damage){
        audioSource.PlayOneShot(damagedAudio);
        switch(variant){
            case EnemyVariant.flesh:
                if(gameConstants.gunElement == GunElement.fire){
                    damage = damage * gameConstants.elementMultiplier;
                }
                break;
            case EnemyVariant.energized:
                if(gameConstants.gunElement == GunElement.shock){
                    damage = damage * gameConstants.elementMultiplier;
                }
                break;
            case EnemyVariant.armored:
                if(gameConstants.gunElement == GunElement.corrosive){
                    damage = damage * gameConstants.elementMultiplier;
                }
                break;
            default:
                break;
        }
        health -= damage;
        Debug.Log("Jumper Health: "+ health + " took " + damage + " damage");
        Debug.Log("Jumper took damage");
        StartCoroutine(Knockback());
        if(health <= 0){
            
            Instantiate(scraps[Random.Range(0,3)], transform.position, Quaternion.identity);
            audioSource.PlayOneShot(deathAudio, 5.0f);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, deathAudio.length);
        }
    }

    private int getState()
    {
        if (Math.Abs(rb.velocity.y)>.01f)
        {
            return (int)MovementState.jumping;
        }

        if (Math.Abs(rb.velocity.x)>.01f)
        {
            return (int)MovementState.running;
        }
        
        return (int)MovementState.idle;
    }

    IEnumerator Knockback()
    {   
        float knockbackDir = Math.Sign(this.transform.position.x - playerObj.transform.position.x);
        float distanceMoved = 0;
        float distanceMovedPerTime = gameConstants.enemyKnockbackDistance / gameConstants.enemyKnockbackTime;
        while (distanceMoved<gameConstants.enemyKnockbackDistance)
        {   
            rb.MovePosition(rb.position + new Vector2(distanceMovedPerTime , 0)*knockbackDir);
            distanceMoved+=distanceMovedPerTime;
            yield return null;
        }
    }
}
