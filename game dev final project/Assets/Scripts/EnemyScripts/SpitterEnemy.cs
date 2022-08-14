using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;

public class SpitterEnemy : MonoBehaviour
{
    public GameConstants gameConstants;
    public  GameObject prefab;
    public GameObject[] scraps;
    public EnemyVariant variant = EnemyVariant.flesh;
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
    
    public void TakeDamage(float damage){
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
        Debug.Log("Spitter Health: "+ health + " took " + damage + " damage");
        Debug.Log("Spitter took damage");
        StartCoroutine(Knockback());
        if(health <= 0){
            Instantiate(scraps[Random.Range(0,3)], transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
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
