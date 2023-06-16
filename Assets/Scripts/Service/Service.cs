using System;
using UnityEngine;
using UnityEngine.Events;

namespace Service
{
    public abstract class Service : ScriptableObject
    {
        [HideInInspector] public UnityEvent<EventArgs> _event;
    }
}