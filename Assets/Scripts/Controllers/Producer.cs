using System;
using Interfaces;
using Service;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public abstract class Producer: MonoBehaviour, IProducer
    {
        public UnityEvent<EventArgs> Event => _event;
        protected UnityEvent<EventArgs> _event;

        protected GameServiceFinder _gameServiceFinder;

        protected virtual void Awake()
        {
            _event = new UnityEvent<EventArgs>();
        }

        protected virtual void Start()
        {
            SubscribeGameServiceFinder();
        }

        protected virtual void OnEnable()
        {
            SubscribeGameServiceFinder();
        }
        
        protected virtual void RegisterProducer()
        {
            _gameServiceFinder.RegisterProducer(this);
        }

        protected void SubscribeGameServiceFinder()
        {
            _gameServiceFinder = FindObjectOfType<GameServiceFinder>();
            if (_gameServiceFinder.Initialized)
            {
                RegisterProducer();
            }
            _gameServiceFinder.Init += RegisterProducer;
        }

        public void Remove_Disable()
        {
            _event.RemoveAllListeners();
        }

        private void OnDisable()
        {
            Remove_Disable();
        }
    }
}