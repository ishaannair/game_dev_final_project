using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpitterEnemy : MonoBehaviour
{
    public GameConstants gameConstants;
    public  GameObject prefab;
    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private Animator anim;
    private float distanceToPlayer;
    private float playerRelativeX;
    private bool attackAvailable = true;
    private float attackCooldown = 5;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = gameConstants.spitterHealth;

    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistanceToPlayer();
        if (Mathf.Abs(distanceToPlayer)<gameConstants.enemySightlines && attackAvailable){
            anim.SetTrigger("Attack");
            Instantiate(prefab, transform.position + new Vector3(1.5f*Mathf.Sign(distanceToPlayer),0,0), Quaternion.identity);
            attackAvailable = false;
            StartCoroutine(SpitCountdown());
        }

        if (health<=0f)
        {   
            anim.SetTrigger("Death");
        }       
       
    }

    IEnumerator SpitCountdown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackAvailable = true;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Melee")){
            // enemyHealth -=10f;
            // Debug.Log("Enemy Health: "+ enemyHealth);
        }
    }
    
    public void TakeDamage(float damage){
        health -= damage;
        Debug.Log("Spitter took damage");
        StartCoroutine(Knockback());
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }

    IEnumerator Knockback()
    {   
        float knockbackDir = Math.Sign(playerObj.transform.position.x - this.transform.position.x);
        float distanceMoved = 0;
        while (distanceMoved<2f)
        {
            rb.MovePosition(rb.position + new Vector2(-0.2f, 0)*knockbackDir);
            distanceMoved+=0.2f;
            yield return null;
        }
    }
}
