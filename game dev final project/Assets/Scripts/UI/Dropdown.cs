using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    public GameObject dropdownImage;
    public Sprite currentSprite;
    public GameObject item1;

    public GameObject item2;
    public GameObject item3;
    public GameObject item4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void switchFunc() {
        dropdownImage.GetComponent<Image>().sprite = currentSprite;
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);
        item4.SetActive(false);


        // if(dropdownImage.activeSelf) {
        //      dropdownImage.GetComponent<Image>().sprite = currentSprite;
        //  }
        //  else {
        //      dropdownImage.SetActive(true);
        //  }
     }
}
