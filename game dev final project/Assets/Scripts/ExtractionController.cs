using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionController : MonoBehaviour
{
    public GameConstants gameConstants;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")){
            int playerScrap = col.gameObject.GetComponent<PlayerController>().scrapCount;
            gameConstants.totalScrap += playerScrap;
            Debug.Log("Extracted " + col.gameObject.GetComponent<PlayerController>().scrapCount);
        }
    }
}
