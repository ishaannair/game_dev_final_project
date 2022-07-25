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
        if(PlayerController.SlashActivated){
            slashAnimator.Play("weapon_sword_side");
              // toggle state
      if (Input.GetKeyDown("a") && faceRightState){
        Debug.Log("left");
          faceRightState = false;
          SwordSprite.flipX = false;
      }

      if (Input.GetKeyDown("d") && !faceRightState){
        Debug.Log("right");
          faceRightState = true;
          SwordSprite.flipX = true;
      }
    }
    }
    }
