using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExploderEnemy : MonoBehaviour
{
    public  GameObject prefab;
    private  GameObject  circle;
    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color color;
    private float distanceToPlayer;
    private float playerRelativeX;
    private float speed = 0.0005f;
    public Vector2 velocity = new Vector2(1, 0);
    public bool isExploding = false;

    public UnityEvent onEnemyHit;

    private float enemyHealth = 20f;

    // Start is called before the first frame update
    void Start()
    {       
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //circle = this.transform.GetChild(0).gameObject;
        color = this.GetComponent<SpriteRenderer>().color;
        color.a = 0.1f;
        sr.color = color;
    }


    void FixedUpdate()
    {   
        if (!isExploding)
        {
            rb.MovePosition(rb.position + Mathf.Sign(distanceToPlayer)*velocity * Time.fixedDeltaTime);
        }
        Debug.Log(enemyHealth);
    }
    // Update is called once per frame
    void Update()
    {   
        
        CalculateDistanceToPlayer();

        if (Mathf.Sign(distanceToPlayer)>0){
            sr.flipX = true;
        } else
        {
            sr.flipX = false;
        }

        if (Mathf.Abs(distanceToPlayer)<4){
            StartCoroutine(Explode());
            isExploding = true;
        } else{
            isExploding = false;
            color.a=0.1f;
            sr.color = color;
        }

        
        if (enemyHealth<=0f)
        {   
            Debug.Log("enemyDied");
            Destroy(gameObject);
        }

       
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
            Debug.Log("Exploded");
            Debug.Log(distanceToPlayer);
            Destroy(gameObject);
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
            onEnemyHit.Invoke();
            enemyHealth -= 10f;
        }
    }

    public void OnHitResponse()
    {
        enemyHealth -= 10f;
        Debug.Log("ENEMY HIT");
        Debug.Log(enemyHealth);
    }

}
