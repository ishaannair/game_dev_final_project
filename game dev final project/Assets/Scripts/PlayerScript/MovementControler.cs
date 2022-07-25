using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControler : MonoBehaviour
{
    private SpriteRenderer objectSprite;
    private bool faceRightState = false;
    public static bool statusPublic = false;
    // Start is called before the first frame update
    void Start()
    {
        objectSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
              // toggle state
      if (Input.GetKeyDown("a") && faceRightState){
        statusPublic=false;
          faceRightState = false;
          objectSprite.flipX = false;
      }

      if (Input.GetKeyDown("d") && !faceRightState){
        statusPublic=true;
          faceRightState = true;
          objectSprite.flipX = true;
      }
    }
}
