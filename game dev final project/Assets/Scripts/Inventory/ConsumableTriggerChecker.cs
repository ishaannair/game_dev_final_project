using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ConsumableTriggerChecker : MonoBehaviour
{
    public Consumables stats;
    public CustomConsumableEvent onCrafted;

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         onCrafted.Invoke(stats,);
	// 		Destroy(this.gameObject);
    //     }
    // }
}
