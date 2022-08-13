using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameConstants gameConstants;
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    public Vector2 velocity0 = new Vector2(10, 0);
    public Vector2 velocity10 = new Vector2(9.84808f, 1.73648f);
    public Vector2 velocity5 = new Vector2(9.96195f, 0.87156f);
    public Vector2 velocity350 = new Vector2(9.84808f, -1.73648f);
    public Vector2 velocity355 = new Vector2(9.96195f, -0.87156f);
    private Vector2 direction;
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
        // Debug.Log(this.gameObject.transform.eulerAngles.z);
        if(gameConstants.gunType == GunType.shotgun){
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
        }else{
            rigidBody.MovePosition(rigidBody.position + direction*velocity0 * Time.fixedDeltaTime);
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
        JumperEnemy jumperScript;
        ExploderEnemy exploderScript;
        SpitterEnemy spitterScript;
        if (col.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Enemy");
            switch(gameConstants.gunType){
                case GunType.blaster:
                    jumperScript = col.gameObject.GetComponent<JumperEnemy>();
                    if(jumperScript == null){
                        exploderScript = col.gameObject.GetComponent<ExploderEnemy>();
                        if(exploderScript == null){
                            spitterScript = col.gameObject.GetComponent<SpitterEnemy>();
                            spitterScript.TakeDamage(gameConstants.blasterDamage * damageMultiplier.Value);
                            break;
                        }
                        exploderScript.TakeDamage(gameConstants.blasterDamage * damageMultiplier.Value);
                        break;
                    }
                    jumperScript.TakeDamage(gameConstants.blasterDamage * damageMultiplier.Value);
                    break;
                case GunType.shotgun:
                    jumperScript = col.gameObject.GetComponent<JumperEnemy>();
                    if(jumperScript == null){
                        exploderScript = col.gameObject.GetComponent<ExploderEnemy>();
                        if(exploderScript == null){
                            spitterScript = col.gameObject.GetComponent<SpitterEnemy>();
                            spitterScript.TakeDamage(gameConstants.blasterDamage * damageMultiplier.Value);
                            break;
                        }
                        exploderScript.TakeDamage(gameConstants.blasterDamage * damageMultiplier.Value);
                        break;
                    }
                    jumperScript.TakeDamage(gameConstants.blasterDamage * damageMultiplier.Value);
                    break;
                default:
                    break;
            }
            this.gameObject.SetActive(false);
            sprite.flipX = false;
        }
        if (col.gameObject.CompareTag("Barrier") || col.gameObject.CompareTag("Ground")){
            Debug.Log("Collided with barrier/ground");
            this.gameObject.SetActive(false);
            sprite.flipX = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}