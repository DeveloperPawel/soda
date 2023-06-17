using System;
using System.Reflection;
using Interfaces;
using Service.Events;
using UnityEngine;

namespace Controllers
{
    public abstract class Controller : MonoBehaviour, IConsumer
    {
        public abstract void EventUpdate(EventArgs args);

        public virtual void Subscribe(IProducer producer)
        {
            producer.Event.AddListener(EventUpdate);
        }
    }
}