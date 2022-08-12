using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour
{
    public GameConstants gameConstants;
    public IntVariable magazine;
    private Slider slider;
    public FloatVariable cooldownTimer;

    void Awake()
    {
        cooldownTimer.SetValue(0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        if(gameConstants.onCooldown){
            if(cooldownTimer.Value == 0){
                switch(gameConstants.gunType){
                    case GunType.blaster:
                        slider.maxValue = gameConstants.blasterReloadTime;
                        cooldownTimer.SetValue(gameConstants.blasterReloadTime);
                        break;
                    case GunType.shotgun:
                        slider.maxValue = gameConstants.shotgunReloadTime;
                        cooldownTimer.SetValue(gameConstants.shotgunReloadTime);
                        break;
                    case GunType.rocketlauncher:
                        slider.maxValue = gameConstants.rocketReloadTime;
                        cooldownTimer.SetValue(gameConstants.rocketReloadTime);
                        break;
                    default:
                        break;
                }
            }
            cooldownTimer.ApplyChange(-Time.fixedDeltaTime);
            slider.value = slider.maxValue - (cooldownTimer.Value - Time.fixedDeltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameConstants.onCooldown){
            cooldownTimer.SetValue(0.0f);
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
