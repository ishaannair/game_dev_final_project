using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ConsumableEvent", menuName = "ScriptableObjects/ConsumableEvent", order = 2)]
public class ConsumableEvent : ScriptableObject
{
    private readonly List<ConsumableEventListener> eventListeners = 
        new List<ConsumableEventListener>();

    public void Raise(Consumables c)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(c);
    }

    public void RegisterListener(ConsumableEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(ConsumableEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}