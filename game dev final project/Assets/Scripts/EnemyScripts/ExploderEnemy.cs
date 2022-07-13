using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float speed = 0.1f;
    public Vector2 velocity = new Vector2(1, 0);


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
        rb.MovePosition(rb.position + Mathf.Sign(distanceToPlayer)*velocity * Time.fixedDeltaTime);
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
        } else{
            if (color.a > 0.2f){
                color.a -= speed * 2* Time.deltaTime;
            } else if (color.a>0.1f){
                color.a=0.1f;
            }
            sr.color = color;
        }

       
    }

    IEnumerator Explode()
    {       
        while (sr.color.a<1){
            color.a +=  speed * Time.deltaTime;
            sr.color = color;
            yield return null;
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            Debug.Log("Collided with Player");
        }
    }
}
