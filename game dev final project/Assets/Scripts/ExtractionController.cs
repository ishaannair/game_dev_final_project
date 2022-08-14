using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionController : MonoBehaviour
{
    private bool used = false;
    public GameConstants gameConstants;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(used){
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 7.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")){
            int playerScrap = col.gameObject.GetComponent<PlayerController>().scrapCount;
            gameConstants.totalScrap += playerScrap;
            Debug.Log("Extracted " + col.gameObject.GetComponent<PlayerController>().scrapCount);
            col.gameObject.GetComponent<PlayerController>().scrapCount = 0;
            used = true;
            StartCoroutine(DestroyDrone());
        }
    }

    public IEnumerator DestroyDrone(){
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
