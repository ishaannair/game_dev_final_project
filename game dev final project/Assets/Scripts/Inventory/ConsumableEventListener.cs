using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CustomConsumableEvent : UnityEvent<Consumables>
{
}

public class ConsumableEventListener : MonoBehaviour
{
    public ConsumableEvent Event;
    public CustomConsumableEvent Response;
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Consumables p)
    {
        Response.Invoke(p);
    }
}