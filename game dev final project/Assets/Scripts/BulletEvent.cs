using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletEvent", menuName = "ScriptableObjects/BulletEvent", order = 3)]
public class BulletEvent : ScriptableObject
{
    private readonly List<BulletEventListener> eventListeners = 
        new List<BulletEventListener>();

    public void Raise(BulletType t)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(t);
    }

    public void RegisterListener(BulletEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(BulletEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
