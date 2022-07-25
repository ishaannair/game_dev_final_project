using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    private GameObject nextSlot;
    private int nextItemNum = 1;
    private string nextItemName;
    public string parent;
    public int maxSlots;

    // Start is called before the first frame update
    void Start()
    {
        nextItemName = "/UI Canvas/" + parent + "/item" + nextItemNum.ToString();
        nextSlot = GameObject.Find(nextItemName);
        GetComponent<RectTransform>().anchoredPosition = nextSlot.GetComponent<RectTransform>().anchoredPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem() {
        if (nextItemNum >= maxSlots) {
            this.gameObject.SetActive(false);
        }
        Transform img = nextSlot.transform.Find("Image");
        img.gameObject.SetActive(true);
        nextItemNum += 1;
        nextItemName = "/UI Canvas/" + parent + "/item" + nextItemNum.ToString();
        nextSlot = GameObject.Find(nextItemName);
        GetComponent<RectTransform>().anchoredPosition = nextSlot.GetComponent<RectTransform>().anchoredPosition;

    }
}
