using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterEnemy : MonoBehaviour
{
    public GameConstants gameConstants;
    public  GameObject prefab;
    private GameObject playerObj = null;
    private Rigidbody2D rb;
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
        health = gameConstants.spitterHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistanceToPlayer();
        if (Mathf.Abs(distanceToPlayer)<20 && attackAvailable){
            Instantiate(prefab, transform.position + new Vector3(3*Mathf.Sign(distanceToPlayer),0,0), Quaternion.identity);
            attackAvailable = false;
            StartCoroutine(JumpCountdown());
        }
       
    }

    IEnumerator JumpCountdown()
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            Debug.Log("Collided with Player");
        }
    }
    
    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }
}
