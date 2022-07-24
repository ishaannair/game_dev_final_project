using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float distanceToPlayer;
    private float playerRelativeX;

    private bool attackAvailable = true;
    private int attackCooldown = 5;
    private float bossHealth;
    private float maxBossHealth = 100;

    // Start is called before the first frame update
    void Start()
    {       
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bossHealth = maxBossHealth;
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
    }


    void cleaveAttack()
    {
        
    }

    void fireballAttack()
    {

    }

    IEnumerator attackCountdown()
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
            bossHealth-=10f;
        }
    }
}
