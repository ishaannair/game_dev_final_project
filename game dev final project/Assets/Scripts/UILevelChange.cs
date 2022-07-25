using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelChange : MonoBehaviour
{
    private int level = 0;
    private Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText = GetComponent<Text>();
        levelText.text = "LVL " + level.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseLevel() {
        level += 1;
        levelText.text = "LVL " + level.ToString();
    }
}
