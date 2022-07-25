using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Vector2 velocity = new Vector2(7, 0);
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); 
    }
    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Barrier") || col.gameObject.CompareTag("Ground")  || col.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with barrier/ground");
            this.gameObject.SetActive(false);
            Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 15);
            Debug.Log(hitColliders);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
