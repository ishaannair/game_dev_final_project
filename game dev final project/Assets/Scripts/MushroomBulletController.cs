using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBulletController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float enemyRange;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void FixedUpdate() {
        float distance = Vector3.Distance(this.transform.parent.position, this.transform.position);
        if (distance >= enemyRange) {
            this.gameObject.SetActive(false);
        }
    }


    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Ground")) {
            this.gameObject.SetActive(false);
        }

        else if (col.gameObject.CompareTag("Player")) {
            Debug.Log("Player damaged");
            this.gameObject.SetActive(false);
        }
    }


    
}
