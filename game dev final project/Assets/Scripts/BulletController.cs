using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Vector2 velocity0 = new Vector2(10, 0);
    public Vector2 velocity10 = new Vector2(9.84808f, 1.73648f);
    public Vector2 velocity5 = new Vector2(9.96195f, 0.87156f);
    public Vector2 velocity350 = new Vector2(9.84808f, -1.73648f);
    public Vector2 velocity355 = new Vector2(9.96195f, -0.87156f);
    private Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if(MovementControler.statusPublic){
            direction=new Vector2(1,1);

        }
        else{
            direction=new Vector2(-1,1);;
        }
    }

    void FixedUpdate()
    {
        Debug.Log(this.gameObject.transform.eulerAngles.z);
        switch(Mathf.RoundToInt(this.gameObject.transform.eulerAngles.z)){
            case 0:
                rigidBody.MovePosition(rigidBody.position + direction*velocity0 * Time.fixedDeltaTime);
                break;
            case 10:
                rigidBody.MovePosition(rigidBody.position + direction*velocity10 * Time.fixedDeltaTime);
                break;
            case 5:
                rigidBody.MovePosition(rigidBody.position +direction* velocity5 * Time.fixedDeltaTime);
                break;
            case 350:
                rigidBody.MovePosition(rigidBody.position + direction*velocity350 * Time.fixedDeltaTime);
                break;
            case 355:
                rigidBody.MovePosition(rigidBody.position +direction* velocity355 * Time.fixedDeltaTime);
                break;
            default:
                rigidBody.MovePosition(rigidBody.position + direction*velocity0 * Time.fixedDeltaTime);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Barrier") || col.gameObject.CompareTag("Ground")){
            Debug.Log("Collided with barrier/ground");
            this.gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Enemy");
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}