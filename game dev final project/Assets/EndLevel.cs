using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }




  void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player")) {
   
            Debug.Log("true end has arrieved");
            SceneManager.LoadScene("Boss 1", LoadSceneMode.Single);

        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
