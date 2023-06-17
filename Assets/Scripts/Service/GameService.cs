using System;
using Interfaces;
using Service.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Service
{
    [CreateAssetMenu(fileName = "GameService", menuName = "Service/Game", order = 0)]
    public class GameService : Service, IConsumer, IProducer, Initializable
    {
        protected bool isInitialized;
        protected override void OnEnable()
        {
            base.OnEnable();
            Initialize();
            isInitialized = true;
        }

        public void EventUpdate(EventArgs args)
        {
            // update the service
        }

        public void Subscribe(IProducer producer)
        {
            producer.Event.AddListener(EventUpdate);
        }

        public UnityEvent<EventArgs> Event => _event;
        public bool Initialized => isInitialized;
        public void Initialize()
        {
            GameServiceStart gameServiceStart = new GameServiceStart();
            _event?.Invoke(gameServiceStart);
        }
    }
}