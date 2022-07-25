using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType{
    blaster = 0,
    shotgun = 1,
}

public class GunController : MonoBehaviour
{
    
    public CustomBulletEvent onShoot;
    public GunType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
                default:
                    Debug.Log("Not one of the gun types");
                    break;
            }
                
        }
    }
}
