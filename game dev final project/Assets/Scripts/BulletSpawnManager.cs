using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawnFromPooler(BulletType i)
    {
        GameObject item = BulletPooler.SharedInstance.GetBullet(i);

        if (item != null)
        {
            // item.transform.position = new Vector3(0, 0, -5);
            item.transform.position = item.transform.parent.position;
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnBullet()
    {
        spawnFromPooler(BulletType.basicBullet);
    }
}
