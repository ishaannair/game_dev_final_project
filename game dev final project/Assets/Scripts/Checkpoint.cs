using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer flagSprite;
    private bool savecheckpoint= false;

    void Start()
    {
        flagSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    // void On(Collision2D col){
    //     if (col.gameObject.CompareTag("Player")) {
    //         savecheckpoint = true;
    //         Debug.Log("true flaaggsss");
    //         transform.localScale = new Vector3(transform.localScale.x, 2F, transform.localScale.z);
    //     }   

    // }

  void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player")) {
            savecheckpoint = true;
            Debug.Log("true flaaggsss");
            transform.localScale = new Vector3(transform.localScale.x, 2F, transform.localScale.z);
        }   
    }



    void Update()
    {
        
    }
}
