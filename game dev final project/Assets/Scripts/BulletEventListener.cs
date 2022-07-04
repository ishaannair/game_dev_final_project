using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomBulletEvent : UnityEvent<BulletType>
{
}
public class BulletEventListener : MonoBehaviour
{
    public BulletEvent Event;
    public CustomBulletEvent Response;
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(BulletType t)
    {
        Response.Invoke(t);
    }
}
