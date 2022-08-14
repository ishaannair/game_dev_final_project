using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector2 velocity = new Vector2(0, 6);
    public GameConstants gameConstants;

    private GameObject playerObj = null;
    public  GameObject prefab;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color color;
    private float distanceToPlayer;
    private bool isBeingSpawned = true;
    private float speed = 0.72f;

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
        transform.rotation *= Quaternion.Euler(0,0,90);
        
        StartCoroutine(Spawning());
    }

    void FixedUpdate()
    {   
        if (!isBeingSpawned){
            rigidBody.MovePosition(rigidBody.position + Mathf.Sign(-3f)*velocity * Time.fixedDeltaTime);
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
        if (col.gameObject.CompareTag("Barrier") || col.gameObject.CompareTag("Ground") ){
            Debug.Log("Collided with barrier");
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player")){
            Debug.Log("Collided with Player");
            Instantiate(prefab, transform.position, Quaternion.identity);
            col.gameObject.GetComponent<PlayerController>().EnemyHit(gameConstants.bossFireballDamage);
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

