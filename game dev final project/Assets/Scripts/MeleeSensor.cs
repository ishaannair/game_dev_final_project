using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSensor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        Debug.Log("Melee Spawned");
    }

    void OnDisable()
    {
        Debug.Log("Melee Despawned");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Hit Enemy");
    }
}
