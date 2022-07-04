using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType{
    basicBullet =  0,
    ricochetBullet =  1
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

    void  Awake()
    {
        SharedInstance = this;
        pooledBullets  =  new  List<ExistingBullet>();
        foreach (Bullet item in  bulletsToPool)
        {
            for (int i =  0; i  <  item.amount; i++)
            {
                // this 'pickup' a local variable, but Unity will not remove it since it exists in the scene
                GameObject pickup = (GameObject)Instantiate(item.prefab);
                pickup.SetActive(false);
                pickup.transform.parent  =  this.transform;
                pooledBullets.Add(new  ExistingBullet(pickup, item.type));
            }
        }
    }

    public  GameObject GetBullet(BulletType type)
    {
        // return inactive pooled object if it matches the type
        for (int i =  0; i  <  pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].gameObject.activeInHierarchy  &&  pooledBullets[i].type  ==  type)
            {
                return  pooledBullets[i].gameObject;
            }
        }

        foreach (Bullet item in bulletsToPool)
        {
            if (item.type == type)
            {
                if (item.expandPool)
                {
                    GameObject pickup = (GameObject)Instantiate(item.prefab);
                    pickup.SetActive(false);
                    pickup.transform.parent  =  this.transform;
                    pooledBullets.Add(new  ExistingBullet(pickup, item.type));
                    return  pickup;
                }
            }
        }
        return null;
    }
}
