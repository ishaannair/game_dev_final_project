using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameConstants gameConstants;
    private Rigidbody2D PlayerBody;
    private bool onGroundState = true;
    public FloatVariable health;
    public FloatVariable decay;

    //Dashing Variables 
     public bool IsDashAvailable = true;
    private bool DashActivated=false;
    //  End
    
    //Slashing Variables 
     public bool IsSlashAvailable = true;
    public static bool SlashActivated=false;
    //  End
    private SpriteRenderer playerSprite;
  
    private  Animator playerAnimator;
    private bool dash;
    
    public static int playerWeapon=1; 
    public GameObject GunHandler;
    private GameObject ParentGameObject;
    public GameObject RocketGun;
    public GameObject ShotGun;
    public GameObject BlasterGun;
    public GameObject Bullets;
    private  GameObject NewGun;
    // Start is called before the first frame update
    void  Start()
    {
        // ParentGameObject= transform;
        playerAnimator  =  GetComponent<Animator>();
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        PlayerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        GameObject NewBullets = Instantiate(Bullets, new Vector3(0, 0, 0), Quaternion.identity);
        health.SetValue(gameConstants.startingTimer);
        decay.SetValue(gameConstants.startingDecay);

        // Spawn guns
        if(gameConstants.gunType == GunType.shotgun){
        Destroy(NewGun);
        NewGun = Instantiate(ShotGun, new Vector3(0, 0, 0), Quaternion.identity);
        NewGun.transform.parent =transform;
        NewGun.transform.localPosition=new Vector3(0, 0.48f, 0);
        }
        else if(gameConstants.gunType == GunType.blaster){
            Destroy(NewGun);
            NewGun = Instantiate(BlasterGun, new Vector3(0, 0, 0), Quaternion.identity);
            NewGun.transform.parent =transform;
            NewGun.transform.localPosition=new Vector3(0, 0.48f, 0);
        }
        else if(gameConstants.gunType == GunType.rocketlauncher){
            Destroy(NewGun);
            NewGun = Instantiate(RocketGun, new Vector3(0, 0, 0), Quaternion.identity);
            NewGun.transform.parent =transform;
            NewGun.transform.localPosition=new Vector3(0, 0.48f, 0);
        }
        NewGun.SetActive(false);
        gameConstants.onCooldown = false;
        
      
        // grandChild= GameObject.Find("Gun");

        // GunHandler = this.gameObject.transform.Find("Gun").gameObject;
        // GunHandler.SetActive(false);

    }
    void FixedUpdate(){
        // health decay calculation
        health.ApplyChange(-(decay.Value * Time.fixedDeltaTime));

        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (PlayerBody.velocity.magnitude < gameConstants.startingPlayerMaxSpeed)
                    PlayerBody.AddForce(movement * gameConstants.playerSpeed);
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // stop
            PlayerBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState){
          PlayerBody.AddForce(Vector2.up * gameConstants.startingPlayerJumpSpeed, ForceMode2D.Impulse);
          onGroundState = false;
         }
        if (Input.GetKeyDown("1")){
            NewGun.SetActive(false);
            playerWeapon=1;
            Debug.Log("weapon 1");
        }
        if (Input.GetKeyDown("2")){
            NewGun.SetActive(true);
            playerWeapon=0;

            Debug.Log("weapon 2");
        }
    }
    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetFloat("xSpeed", Mathf.Abs(PlayerBody.velocity.x));
        playerAnimator.SetBool("OnGround", onGroundState);
        playerAnimator.SetBool("OnDash", DashActivated);
        playerAnimator.SetBool("Swordcut", SlashActivated);
              // toggle state
        // Slashing
        if (Input.GetMouseButtonDown(0)){
            if (playerWeapon==1){
            ActivateSlash();
            Debug.Log("Pressed primary button.");
            }
            // if (playerWeapon==1){
            //     playerAnimator.SetBool("Swordcut", SlashActivated);
            // }
            // if(playerWeapon==2){
            //     playerAnimator.SetBool("SycthCut", SlashActivated);
            // }
        }
        // if(Input.GetKeyDown("r")){
        // Destroy(NewGun);
        //  NewGun = Instantiate(ShotGun, new Vector3(0, 0, 0), Quaternion.identity);
        // NewGun.transform.parent =transform;
        // NewGun.transform.localPosition=new Vector3(0, 0.48f, 0);
        // }
        // if(Input.GetKeyDown("t")){
        //     Destroy(NewGun);
        //     NewGun = Instantiate(BlasterGun, new Vector3(0, 0, 0), Quaternion.identity);
        //     NewGun.transform.parent =transform;
        //     NewGun.transform.localPosition=new Vector3(0, 0.48f, 0);
        // }
        // if(Input.GetKeyDown("y")){
        //     Destroy(NewGun);
        //     NewGun = Instantiate(RocketGun, new Vector3(0, 0, 0), Quaternion.identity);
        //     NewGun.transform.parent =transform;
        //     NewGun.transform.localPosition=new Vector3(0, 0.48f, 0);
        // }
        //Dashing 
        if (Input.GetKeyDown("e")){
            // dashing skill
                ActivateDodge();
            }
      if (Input.GetKeyDown("a") && gameConstants.playerFaceRightState){
          gameConstants.playerFaceRightState = false;
          playerSprite.flipX = false;
      }

      if (Input.GetKeyDown("d") && !gameConstants.playerFaceRightState){
          gameConstants.playerFaceRightState = true;
          playerSprite.flipX = true;
      }
    //   weapon swap

    }



  // called when the cube hits the floor
  void OnCollisionEnter2D(Collision2D col)
  {
      if (col.gameObject.CompareTag("Ground")) {
      onGroundState = true;
      Debug.Log("true");
      }
  }


//   Custom functions
// attacking -->
    private void ActivateDodge(){
         // if not available to use (still cooling down) just exit
         Debug.Log("dash");

         if (IsDashAvailable == false)// check if abilty is ready to deploy. false means no.
         {
             return;
         }
         DashActivated=true;// a variable that counters the shift key to prevent any clashes within the 2 abilities.
         // made it here then ability is available to use...

        gameConstants.startingPlayerMaxSpeed = 30f;
         // start the cooldown timer
         StartCoroutine(D_Activation());//used to deploy the ability.
         StartCoroutine(D_StartCooldown());// After ablity is deployed put certain time for cooldown.
        
     }
    public IEnumerator D_Activation()
     {
         IsDashAvailable = false;
         yield return new WaitForSeconds(gameConstants.playerDashDuration);
         gameConstants.startingPlayerMaxSpeed = 10f;
         DashActivated=false;
         
     }
          public IEnumerator D_StartCooldown()
     {
         IsDashAvailable = false;
         yield return new WaitForSeconds(gameConstants.playerDashCooldownDuration);
        
         IsDashAvailable = true;
     }
// Slashing -->
    private void ActivateSlash(){
         // if not available to use (still cooling down) just exit
         Debug.Log("Slash");

         if (IsSlashAvailable == false)// check if abilty is ready to deploy. false means no.
         {
             return;
         }
         SlashActivated=true;// a variable that counters the shift key to prevent any clashes within the 2 abilities.
         // made it here then ability is available to use...
        
     
         // start the cooldown timer
         StartCoroutine(S_Activation());//used to deploy the ability.
        //  StartCoroutine(S_StartCooldown());// After ablity is deployed put certain time for cooldown.
        
     }
    public IEnumerator S_Activation()
     {
        
         IsSlashAvailable = false;
         yield return new WaitForSeconds(gameConstants.playerSlashDuration);

         SlashActivated=false;
         IsSlashAvailable = true;
         
     }

    public void TakeDamage(float damage){
        decay.ApplyChange(damage);
    }
    //       public IEnumerator S_StartCooldown()
    //  {
    //      IsSlashAvailable = false;
    //      yield return new WaitForSeconds(gameConstants.playerSlashCooldownDuration);
        
         
    //  }
}
