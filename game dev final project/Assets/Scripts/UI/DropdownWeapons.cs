using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownWeapons : MonoBehaviour
{
    public GameObject dropdownImage;
    public Sprite currentSprite;
    public GameObject item1;

    public GameObject item2;
    public GameObject item3;
    public GunType currGunType;
    public GameConstants gameConstants;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void switchWeap() {
        dropdownImage.GetComponent<Image>().sprite = currentSprite;
        gameConstants.gunType = currGunType;
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);


        // if(dropdownImage.activeSelf) {
        //      dropdownImage.GetComponent<Image>().sprite = currentSprite;
        //  }
        //  else {
        //      dropdownImage.SetActive(true);
        //  }
     }
}
