using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Vector2 velocity = new Vector2(3, 0);

    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {   
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        CalculateDistanceToPlayer();
    }

    void FixedUpdate()
    {   
        rigidBody.MovePosition(rigidBody.position + Mathf.Sign(distanceToPlayer)*velocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.gameObject.CompareTag("Barrier")){
            Debug.Log("Collided with barrier");
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player")){
            Debug.Log("Collided with Player");
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

