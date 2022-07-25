using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class JumperEnemy : MonoBehaviour
{
    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private float distanceToPlayer;
    private float playerRelativeX;
    private int state;

    private bool jumpAvailable = true;
    private bool moveAvailable = true;
    private int jumpCooldown = 5;
    public float velocity = 3f;
    private float maxHealth = 50f;
    private float currentHealth;
    
    private enum MovementState {idle, running, jumping} 

    // Start is called before the first frame update
    void Start()
    {       
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
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

        if (Mathf.Abs(distanceToPlayer)>=10)
        {
            rb.velocity = new Vector2(Mathf.Sign(distanceToPlayer) * velocity, rb.velocity.y);
        } else
        {
            if (jumpAvailable)
            {
                rb.AddForce(new Vector3(5*distanceToPlayer,60,0), ForceMode2D.Impulse);
                jumpAvailable = false;
                StartCoroutine(JumpCountdown());
            }
        }

        if (currentHealth<=0f)
        {   
            Debug.Log("enemyDied");
            Destroy(gameObject);
        }       
    }

    IEnumerator JumpCountdown()
    {
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            Debug.Log("Collided with Player");
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Melee")){
            currentHealth-=10f;
            Debug.Log("Enemy's current health: " + currentHealth);
        }
    }
}
