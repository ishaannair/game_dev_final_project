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
    public void spawnFromPooler(BulletType i)
    {
        List<GameObject> items = BulletPooler.SharedInstance.GetBullet(i);

        if (items != null)
        {
            int index = 0;
            foreach (GameObject item in items){
                Debug.Log(index);
                // item.transform.position = new Vector3(0, 0, -5);
                item.transform.localPosition = new Vector3(0, 0, 0);
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
    }
}