using System;
using UnityEngine;
using UnityEngine.Events;

namespace Service
{
    public abstract class Service : ScriptableObject
    {
        [HideInInspector] private UnityEvent<EventArgs> _event;
    }
}