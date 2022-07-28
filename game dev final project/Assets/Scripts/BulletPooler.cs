using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType{
    blasterBullet =  0,
    shotgunBullet =  1,
    rocket = 2,
}

[System.Serializable]
public class Bullet
{
    public  int amount;
    public  GameObject prefab;
    public  bool expandPool;
    public  BulletType type;
}

public  class ExistingBullet
{
    public  GameObject gameObject;
    public  BulletType type;

    // constructor
    public  ExistingBullet(GameObject gameObject, BulletType type){
        // reference input
        this.gameObject  =  gameObject;
        this.type  =  type;
    }
}
public class BulletPooler : MonoBehaviour
{
    public  List<Bullet> bulletsToPool;
    public  List<ExistingBullet> pooledBullets;
    public static BulletPooler SharedInstance;
    public GameConstants gameConstants;
    private bool prevFaceRightState;

    void  Awake()
    {
        SharedInstance = this;
        prevFaceRightState = gameConstants.playerFaceRightState;
        pooledBullets  =  new  List<ExistingBullet>();
        foreach (Bullet item in  bulletsToPool)
        {
            for (int i =  0; i  <  item.amount; i++)
            {
                GameObject pickup = (GameObject)Instantiate(item.prefab);
                pickup.SetActive(false);
                pickup.transform.parent  =  this.transform;
                pooledBullets.Add(new  ExistingBullet(pickup, item.type));
            }
        }
    }

    void Update()
    {
        switch(gameConstants.gunType)
            {
                case GunType.blaster:
                    if(gameConstants.playerFaceRightState){
                        this.transform.localPosition = new Vector3(-0.395f, 0.035f, 0.0f);
                    }else{
                        this.transform.localPosition = new Vector3(0.403f, 0.035f, 0.0f);
                    }
                    break;
                case GunType.shotgun:
                    if(gameConstants.playerFaceRightState){
                        this.transform.localPosition = gameConstants.shotgunRightPool;
                        if(prevFaceRightState != gameConstants.playerFaceRightState){
                            foreach(Transform child in this.transform){
                                Vector3 bulletPoolOffset = gameConstants.shotgunRightPool - gameConstants.shotgunLeftPool + gameConstants.shotgunLeftSprite - gameConstants.shotgunRightSprite;
                                Debug.Log(bulletPoolOffset);
                                child.position += bulletPoolOffset;
                            }
                        }
                    }else{
                        this.transform.localPosition = gameConstants.shotgunLeftPool;
                        if(prevFaceRightState != gameConstants.playerFaceRightState){
                            foreach(Transform child in this.transform){
                                Vector3 bulletPoolOffset = gameConstants.shotgunLeftPool - gameConstants.shotgunRightPool + gameConstants.shotgunRightSprite - gameConstants.shotgunLeftSprite;
                                Debug.Log(bulletPoolOffset);
                                child.position += bulletPoolOffset;
                            }
                        }
                    }
                    break;
                case GunType.rocketlauncher:
                    if(gameConstants.playerFaceRightState){
                        this.transform.localPosition = new Vector3(-0.395f, 0.035f, 0.0f);
                    }else{
                        this.transform.localPosition = new Vector3(0.403f, 0.035f, 0.0f);
                    }
                    break;
                default:
                    break;
            }
        prevFaceRightState = gameConstants.playerFaceRightState;
    }

    public  List<GameObject> GetBullet(BulletType type)
    {
        List<GameObject> returnedBullets = new List<GameObject>();
        int bulletCount;
        switch(type){
            case BulletType.shotgunBullet:
                bulletCount = 5;
                break;
            default:
                bulletCount = 1;
                break;
        }
        Debug.Log(bulletCount);
        // return inactive pooled object if it matches the type
        for (int i =  0; i  <  pooledBullets.Count; i++)
        {
            Debug.Log("Looking for Bullet for loop");
            if (!pooledBullets[i].gameObject.activeInHierarchy  &&  pooledBullets[i].type  ==  type)
            {
                returnedBullets.Add(pooledBullets[i].gameObject);
                if(returnedBullets.Count == bulletCount){
                    return returnedBullets;
                }
            }
        }
        return null;

        // foreach (Bullet item in bulletsToPool)
        // {
        //     if (item.type == type)
        //     {
        //         if (item.expandPool)
        //         {
        //             GameObject pickup = (GameObject)Instantiate(item.prefab);
        //             pickup.SetActive(false);
        //             pickup.transform.parent  =  this.transform;
        //             pooledBullets.Add(new  ExistingBullet(pickup, item.type));
        //             return  pickup;
        //         }
        //     }
        // }
    }
}
