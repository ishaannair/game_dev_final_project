using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour
{
    public GameConstants gameConstants;
    public IntVariable magazine;
    private Slider slider;
    private float cooldownTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        if(gameConstants.onCooldown){
            if(cooldownTimer == 0){
                switch(gameConstants.gunType){
                    case GunType.blaster:
                        slider.maxValue = gameConstants.blasterReloadTime;
                        cooldownTimer = gameConstants.blasterReloadTime;
                        break;
                    case GunType.shotgun:
                        slider.maxValue = gameConstants.shotgunReloadTime;
                        cooldownTimer = gameConstants.shotgunReloadTime;
                        break;
                    case GunType.rocketlauncher:
                        slider.maxValue = gameConstants.rocketReloadTime;
                        cooldownTimer = gameConstants.rocketReloadTime;
                        break;
                    default:
                        break;
                }
            }
            cooldownTimer -= Time.fixedDeltaTime;
            slider.value = slider.maxValue - (cooldownTimer - Time.fixedDeltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameConstants.onCooldown){
            cooldownTimer = 0;
            switch(gameConstants.gunType){
                case GunType.blaster:
                    slider.maxValue = gameConstants.blasterAmmoClip;
                    break;
                case GunType.shotgun:
                    slider.maxValue = gameConstants.shotgunAmmoClip;
                    break;
                case GunType.rocketlauncher:
                    slider.maxValue = gameConstants.rocketAmmoClip;
                    break;
                default:
                    break;
            }
            slider.value = magazine.Value;
        }
    }
}
