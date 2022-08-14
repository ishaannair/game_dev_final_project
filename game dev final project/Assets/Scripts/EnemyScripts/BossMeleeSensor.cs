using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeSensor : MonoBehaviour
{
    public GameConstants gameConstants;
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().EnemyHit(gameConstants.bossMeleeDamage);
            Debug.Log("Damage Player via Melee");
            gameObject.SetActive(false);
        }
    }
}