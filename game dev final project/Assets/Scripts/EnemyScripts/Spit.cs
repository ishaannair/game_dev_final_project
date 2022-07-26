using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Vector2 velocity = new Vector2(3, 0);

    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color color;
    private float distanceToPlayer;
    private bool isBeingSpawned = true;
    private float speed = 1.3f;
    // Start is called before the first frame update
    void Start()
    {   
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        CalculateDistanceToPlayer();
        color = this.GetComponent<SpriteRenderer>().color;
        color.a = 0.1f;
        sr.color = color;
        StartCoroutine(Spawning());
    }

    void FixedUpdate()
    {   
        if (!isBeingSpawned){
            rigidBody.MovePosition(rigidBody.position + Mathf.Sign(distanceToPlayer)*velocity * Time.fixedDeltaTime);
        }
    }

    IEnumerator Spawning()
    {
        while (sr.color.a<1){
            color.a +=  speed * Time.deltaTime;
            sr.color = color;
            yield return null;
        }
        isBeingSpawned = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.gameObject.CompareTag("Barrier")){
            Debug.Log("Collided with barrier");
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player")){
            Debug.Log("Collided with Player");
            col.gameObject.GetComponent<PlayerController>().TakeDamage(1);
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
}

