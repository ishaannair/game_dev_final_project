using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;
    public IntVariable magazine;
    private float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        switch(gameConstants.gunType){
            case GunType.blaster:
                magazine.SetValue(gameConstants.blasterAmmoClip);
                cooldownTimer = gameConstants.blasterReloadTime;
                break;
            case GunType.shotgun:
                magazine.SetValue(gameConstants.shotgunAmmoClip);
                cooldownTimer = gameConstants.shotgunReloadTime;
                break;
            case GunType.rocketlauncher:
                magazine.SetValue(gameConstants.rocketAmmoClip);
                cooldownTimer = gameConstants.rocketReloadTime;
                break;
            default:
                magazine.SetValue(0);
                cooldownTimer = 0;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawnFromPooler(BulletType i)
    {
        if(gameConstants.onCooldown){
            return;
        }
        List<GameObject> items = BulletPooler.SharedInstance.GetBullet(i);

        if (items != null)
        {
            int index = 0;
            foreach (GameObject item in items){
                Debug.Log(index);
                // item.transform.position = new Vector3(0, 0, -5);
                if(i == BulletType.shotgunBullet){
                    if(gameConstants.playerFaceRightState){
                        item.transform.localPosition = gameConstants.shotgunRightPool;
                    }else{
                        item.transform.localPosition = gameConstants.shotgunLeftPool;
                    }
                }else if(i == BulletType.blasterBullet){
                    if(gameConstants.playerFaceRightState){
                        item.transform.localPosition = gameConstants.blasterRightPool;
                    }else{
                        item.transform.localPosition = gameConstants.blasterLeftPool;
                    }
                }else if(i == BulletType.rocket){
                    if(gameConstants.playerFaceRightState){
                        item.transform.localPosition = gameConstants.rocketRightPool;
                    }else{
                        item.transform.localPosition = gameConstants.rocketLeftPool;
                    }
                }
                if(i == BulletType.shotgunBullet){
                    switch(index){
                        case 1:
                            item.transform.eulerAngles = new Vector3(0, 0, 5.0f);
                            break;
                        case 2:
                            item.transform.eulerAngles = new Vector3(0, 0, 10.0f);
                            break;
                        case 3:
                            item.transform.eulerAngles = new Vector3(0, 0, 355.0f);
                            break;
                        case 4:
                            item.transform.eulerAngles = new Vector3(0, 0, 350.0f);
                            break;
                        default:
                            item.transform.eulerAngles = new Vector3(0, 0, 0);
                            break;
                    }       
                }
                item.SetActive(true);
                index++;
            }
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
        magazine.ApplyChange(-1);
        if(magazine.Value == 0){
            switch(gameConstants.gunType){
                case GunType.blaster:
                    magazine.SetValue(gameConstants.blasterAmmoClip);
                    break;
                case GunType.shotgun:
                    magazine.SetValue(gameConstants.shotgunAmmoClip);
                    break;
                case GunType.rocketlauncher:
                    magazine.SetValue(gameConstants.rocketAmmoClip);
                    break;
                default:
                    magazine.SetValue(0);
                    break;
            }
            gameConstants.onCooldown = true;
            StartCoroutine(gunCooldown());
        }
        Debug.Log(magazine);
    }
    IEnumerator gunCooldown(){
        yield return new WaitForSeconds(cooldownTimer);
        gameConstants.onCooldown = false;
    }
}
