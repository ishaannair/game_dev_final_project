using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameConstants gameConstants;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().EnemyHit(gameConstants.jumperDamage);
        }
    }
}
