using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
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
              Time.timeScale = 1.0f;
            //   SceneManager.LoadScene("Another Map 1", LoadSceneMode.Single);
          
      }
  }
}
