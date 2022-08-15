using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    void OnEnable()
    {
        Time.timeScale=0;
    }

    void OnDisable()
    {
        Time.timeScale=1;
    }

}
