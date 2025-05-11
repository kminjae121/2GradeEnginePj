using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractionObj : MonoBehaviour
{
    public UnityEvent InteractEvent;


    protected virtual void Awake()
    {
        InteractEvent.AddListener(HandleInteractEvent);
    }


    public abstract void HandleInteractEvent();
}
