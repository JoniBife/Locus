﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    { 
        Event.RegisterListener(this); 
    }

    private void OnDisable()
    { 
        Event.UnregisterListener(this); 
    }

    public void OnEventRaised()
    { 
        Response.Invoke();
    }
}

public class GameEventListener<T> : MonoBehaviour
{
    public GameEvent<T> Event;
    public UnityEvent<T> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(T arg)
    {
        Response.Invoke(arg);
    }
}


