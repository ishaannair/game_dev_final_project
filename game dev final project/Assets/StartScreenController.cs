using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        Time.timeScale = 0.0f;
        Debug.Log("paused game" );
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButtonClicked()
  {
    Debug.Log("test" );
      foreach (Transform eachChild in transform)
      {
          
          
              Debug.Log("Child found. Name: " + eachChild.name);
              // disable them
              eachChild.gameObject.SetActive(false);
              
          
      }
  }
}
