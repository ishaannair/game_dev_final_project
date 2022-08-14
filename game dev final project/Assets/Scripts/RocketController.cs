using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private bool checkdirection;
    public Vector2 velocity = new Vector2(7, 0);
    private SpriteRenderer sprite;
    public GameConstants gameConstants;
    private Vector2 direction;
    public GameObject impact;
    public FloatVariable damageMultiplier;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        if(gameConstants.playerFaceRightState){
            direction=new Vector2(1,1);
            sprite.flipX = false;
        }
        else{
            direction=new Vector2(-1,1);
            sprite.flipX = true;
        }
    }
    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + velocity * direction * Time.fixedDeltaTime);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Barrier") || col.gameObject.CompareTag("Enemy")){
            Instantiate(impact, this.transform.position, Quaternion.identity);
            Debug.Log("Collided with something: " + col.gameObject.tag);
            this.gameObject.SetActive(false);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), 2.25f);
            foreach(Collider2D hitCol in hitColliders){
                if(hitCol.CompareTag("Enemy")){
                    JumperEnemy jumperScript;
                    ExploderEnemy exploderScript;
                    SpitterEnemy spitterScript;
                    BossMini bossMiniScript;
                    Boss bossScript;
                    bossMiniScript = col.gameObject.GetComponent<BossMini>();
                    if(bossMiniScript == null){
                        bossScript = col.gameObject.GetComponent<Boss>();
                        if (bossScript == null){
                            jumperScript = col.gameObject.GetComponent<JumperEnemy>();
                            if(jumperScript == null){
                                exploderScript = col.gameObject.GetComponent<ExploderEnemy>();
                                if(exploderScript == null){
                                    spitterScript = col.gameObject.GetComponent<SpitterEnemy>();
                                    spitterScript.TakeDamage(gameConstants.rocketDamage * damageMultiplier.Value);
                                    continue;
                                }
                                exploderScript.TakeDamage(gameConstants.rocketDamage * damageMultiplier.Value);
                                continue;
                            }
                            jumperScript.TakeDamage(gameConstants.rocketDamage * damageMultiplier.Value);
                            continue;
                        }
                        bossScript.TakeDamage(gameConstants.rocketDamage * damageMultiplier.Value);
                        continue;
                    }
                    bossMiniScript.TakeDamage(gameConstants.rocketDamage * damageMultiplier.Value);
                    continue;
                }
            }
            // Debug.Log(this.gameObject.transform.position);
            // Debug.Log(hitColliders.Length);
        }
    }
}
