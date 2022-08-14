using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    private bool spawnFinalBoss = false; 
    public GameObject boss;
    public GameConstants gameConstants;
    
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnBoss()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("BossSpawnPoint");
        Instantiate(boss, spawnPoint.transform.position, Quaternion.identity);
    }

    public void BossMiniDeathResponse()
    {   
        Debug.Log("Mini Boss Death");
        if (!gameConstants.bigBossHasSpawned){
            gameConstants.bigBossHasSpawned=true;
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            CameraController cameraScript = mainCamera.GetComponent<CameraController>();
            
            StartCoroutine(DestroyStage());
            cameraScript.Shake();
            
        }

    }

    private IEnumerator DestroyStage()
    {
        GameObject destroyables = GameObject.FindGameObjectWithTag("DestroyablePlatforms");
        while (destroyables.transform.childCount>0)
        {   
            GameObject child = destroyables.transform.GetChild(0).gameObject;
            Destroyables childScript = child.GetComponent<Destroyables>();
            childScript.Break();
            yield return new WaitForSeconds(0.5f);
            Debug.Log(destroyables.transform.childCount);
        }
        
        SpawnBoss();
    }

    
}
