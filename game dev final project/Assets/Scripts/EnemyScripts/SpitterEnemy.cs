using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpitterEnemy : MonoBehaviour
{

    public  GameObject prefab;
    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private Animator anim;
    private float distanceToPlayer;
    private float playerRelativeX;
    private bool attackAvailable = true;
    private float attackCooldown = 5;
    private float enemyHealth;
    private float enemyMaxHealth = 20f;
    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        anim = GetComponent<Animator>();
        enemyHealth = enemyMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistanceToPlayer();
        if (Mathf.Abs(distanceToPlayer)<15 && attackAvailable){
            anim.SetTrigger("Attack");
            Instantiate(prefab, transform.position + new Vector3(1.5f*Mathf.Sign(distanceToPlayer),0,0), Quaternion.identity);
            attackAvailable = false;
            StartCoroutine(SpitCountdown());
        }

        if (enemyHealth<=0f)
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
<<<<<<< Updated upstream
            enemyHealth -=10f;
            Debug.Log("Enemy Health: "+ enemyHealth);
=======
            // enemyHealth -=10f;
            // Debug.Log("Enemy Health: "+ enemyHealth);
        }
    }
    
    public void TakeDamage(float damage){
        health -= damage;
        StartCoroutine(Knockback());
        if(health <= 0){
            Destroy(this.gameObject);
>>>>>>> Stashed changes
        }
    }

    IEnumerator Knockback()
    {   
        float time = 0.2f;
        while (time>0){
            time -=  Time.deltaTime;
            transform.position += new Vector3(Math.Sign(distanceToPlayer)*10f*Time.deltaTime,0,0);
            yield return null;
        }
    }
}
