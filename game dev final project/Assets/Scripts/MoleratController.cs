using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleratController : MonoBehaviour
{

    private Vector3 playerPos;
    private Vector3 undergroundPos;
    private Vector3 groundPos;
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private float force = 5f;
    private float currTransparency = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        groundPos = GameObject.Find("Ground").transform.position;
        StartCoroutine(findPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        currTransparency += Time.fixedDeltaTime;
        sprite.color = new Color(1,1,1, currTransparency);
        
    }

    IEnumerator findPlayer() {
        while (true) {
            sprite.enabled = true;
            onGround = false;
            currTransparency = 0.1f;
            sprite.color = new Color(1,1,1,currTransparency);
            playerPos = GameObject.Find("HeroKnight").transform.position;
            undergroundPos = GameObject.Find("Underground").transform.position;
            rigidBody.position = new Vector2(playerPos.x, undergroundPos.y+0.5f);
            yield return new WaitForSeconds(2f);
            rigidBody.AddForce(Vector2.up*force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(5f);

            sprite.enabled = false;
            yield return new WaitForSeconds(2f);
        }
        
    }


    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            Debug.Log("Player killed");
        }
    }
}
