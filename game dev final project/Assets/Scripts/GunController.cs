using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType{
    blaster = 0,
    shotgun = 1,
    rocketlauncher = 2,
}

public enum GunElement{
    neutral = 0,
    fire = 1,
    shock = 2,
    corrosive = 3,
}

public class GunController : MonoBehaviour
{
    public CustomBulletEvent onShoot;
    public GameConstants gameConstants;
    public GunType type;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        gameConstants.onCooldown=false;
        gameConstants.gunType = type;
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(type)
            {
                case GunType.blaster:
                    sprite.flipX = gameConstants.playerFaceRightState;
                    if(gameConstants.playerFaceRightState){
                        this.transform.localPosition = gameConstants.blasterRightSprite;
                    }else{
                        this.transform.localPosition = gameConstants.blasterLeftSprite;
                    }
                    break;
                case GunType.shotgun:
                    sprite.flipX = gameConstants.playerFaceRightState;
                    if(gameConstants.playerFaceRightState){
                        this.transform.localPosition = gameConstants.shotgunRightSprite;
                    }else{
                        this.transform.localPosition = gameConstants.shotgunLeftSprite;
                    }
                    break;
                case GunType.rocketlauncher:
                    sprite.flipX = gameConstants.playerFaceRightState;
                    if(gameConstants.playerFaceRightState){
                        this.transform.localPosition = gameConstants.rocketRightSprite;
                    }else{
                        this.transform.localPosition = gameConstants.rocketLeftSprite;
                    }
                    break;
                default:
                    sprite.flipX = gameConstants.playerFaceRightState;
                    break;
            }
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            switch(type)
            {
                case GunType.blaster:
                    Debug.Log("Shooting blaster");
                    onShoot.Invoke(BulletType.blasterBullet);
                    break;
                case GunType.shotgun:
                    Debug.Log("Shooting shotgun");
                    onShoot.Invoke(BulletType.shotgunBullet);
                    break;
                case GunType.rocketlauncher:
                    Debug.Log("Shooting rocket launcher");
                    onShoot.Invoke(BulletType.rocket);
                    break;
                default:
                    Debug.Log("Not one of the gun types");
                    break;
            }
                
        }
    }
}
