using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExploderEnemy : MonoBehaviour
{
    public GameConstants gameConstants;
    public  GameObject prefab;
    private  GameObject  circle;
    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color color;
    private Animator anim;
    private float distanceToPlayer;
    private float playerRelativeX;
    private float speed = 0.025f;
    public Vector2 velocity = new Vector2(1, 0);
    public bool isExploding = false;
    public EnemyVariant variant = EnemyVariant.flesh;
    public UnityEvent onEnemyHit;

    private float health;

    // Start is called before the first frame update
    void Start()
    {       
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //circle = this.transform.GetChild(0).gameObject;
        color = this.GetComponent<SpriteRenderer>().color;
        color.a = 0.1f;
        sr.color = color;
        health = gameConstants.exploderHealth;
    }


    void FixedUpdate()
    {   
        if (!isExploding)
        {
            rb.MovePosition(rb.position + Mathf.Sign(distanceToPlayer)*velocity * Time.fixedDeltaTime);
        }
       //Debug.Log("FU"+ enemyHealth);
        
    }
    // Update is called once per frame
    void Update()
    {   
        
        CalculateDistanceToPlayer();

        if (Mathf.Sign(distanceToPlayer)>0){
            sr.flipX = false;
        } else
        {
            sr.flipX = true;
        }

        if (Mathf.Abs(distanceToPlayer)<3){
            StartCoroutine(Explode());
            isExploding = true;
        } else{
            isExploding = false;
            color.a=0.1f;
            sr.color = color;
        }

        
        if (health<=0f)
        {   
            Debug.Log("enemyDied");
            Destroy(gameObject);
        }       
        //Debug.Log("U"+ enemyHealth);
    }

    IEnumerator Explode()
    {       
        while (sr.color.a<1 ){
            color.a +=  speed * Time.deltaTime;
            sr.color = color;
            yield return null;
        }

        if (isExploding)
        {   
            anim.SetTrigger("Explode");
        }
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
        Debug.Log("Exploder Health: "+ health + " took " + damage + " damage");
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }
}
