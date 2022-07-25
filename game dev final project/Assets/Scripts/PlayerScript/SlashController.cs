using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    // Start is called before the first frame update
     private  Animator slashAnimator;
    private SpriteRenderer SwordSprite;
    private bool faceRightState = false;   
    void Start()
    {
        slashAnimator  =  GetComponent<Animator>();
        SwordSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.SlashActivated && PlayerController.playerWeapon==1){
            slashAnimator.Play("weapon_sword_side");
            }
              // toggle state
      if (Input.GetKeyDown("a") && faceRightState){
 
          faceRightState = false;
          SwordSprite.flipX = false;
      }

      if (Input.GetKeyDown("d") && !faceRightState){

          faceRightState = true;
          SwordSprite.flipX = true;
      }
    
    }
    }
