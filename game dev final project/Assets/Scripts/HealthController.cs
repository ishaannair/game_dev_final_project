using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public FloatVariable health;
    public GameConstants gameConstants;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
        slider.maxValue = gameConstants.startingTimer;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health.Value;
    }
}
