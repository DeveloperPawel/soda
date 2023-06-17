using System;
using UnityEngine;
using UnityEngine.Events;

namespace Service
{
    public abstract class Service : ScriptableObject
    {
        protected UnityEvent<EventArgs> _event;

        protected virtual void OnEnable()
        {
            if (_event == null) _event = new UnityEvent<EventArgs>();
        }
    }
}