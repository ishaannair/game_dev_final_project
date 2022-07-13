using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemy : MonoBehaviour
{
    private GameObject playerObj = null;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float distanceToPlayer;
    private float playerRelativeX;

    private bool jumpAvailable = true;
    private int jumpCooldown = 5;

    // Start is called before the first frame update
    void Start()
    {       
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

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

        if (Mathf.Abs(distanceToPlayer)<10 && jumpAvailable){
            rb.AddForce(new Vector3(5*distanceToPlayer,60,0), ForceMode2D.Impulse);
            jumpAvailable = false;
            StartCoroutine(JumpCountdown());
        }
    }

    IEnumerator JumpCountdown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        jumpAvailable = true;
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
