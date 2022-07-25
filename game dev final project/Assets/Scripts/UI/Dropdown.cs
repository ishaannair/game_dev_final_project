using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    public GameObject dropdownImage;
    public Sprite currentSprite;
    public Consumables consumable;
    public GameObject item1;

    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public CustomConsumableEvent onCraft;
    public List<Consumables> consumablesList;
    public int dropdownIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void switchFunc() {
        for (int i = 0; i < 4; i ++){
            if (currentSprite == consumablesList[i].consumablesSprite){
                // Debug.Log("Same Sprite");
                onCraft.Invoke(consumablesList[i], dropdownIndex);
            }
        }
        // dropdownImage.GetComponent<Image>().sprite = currentSprite;
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
