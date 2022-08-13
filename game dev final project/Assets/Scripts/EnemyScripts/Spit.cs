using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float velocity = 5f;

    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color color;
    private Vector2 pathToPlayer;
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
            rigidBody.MovePosition(rigidBody.position + pathToPlayer*velocity * Time.fixedDeltaTime);
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
        pathToPlayer = new Vector2(xDist,yDist).normalized;
    }
}

