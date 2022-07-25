using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private bool checkdirection;
    public Vector2 velocity = new Vector2(7, 0);
    public SpriteRenderer bulletRender;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); 
        checkdirection=MovementControler.statusPublic;
        bulletRender = GetComponent<SpriteRenderer>();
        
    }
    void FixedUpdate()
    {
        if (checkdirection){
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
     
        }
        else{
            rigidBody.MovePosition(rigidBody.position -velocity * Time.fixedDeltaTime);
            
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Barrier") || col.gameObject.CompareTag("Ground")  || col.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with something");
            this.gameObject.SetActive(false);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), 2.25f);
            // Debug.Log(this.gameObject.transform.position);
            // Debug.Log(hitColliders.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkdirection){
            bulletRender.flipX = false;
        }
        else{
            bulletRender.flipX = true;
        }
    }
}
