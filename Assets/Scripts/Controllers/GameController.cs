using System;
using System.Reflection;
using Interfaces;
using Service;
using Service.Events;
using UnityEngine;

namespace Controllers
{
    public class GameController : Controller
    {
        [SerializeField] protected GameService gameService;

        protected virtual void OnEnable()
        {
            Subscribe(gameService);
        }

        public virtual void Consume(GameServiceStart gameServiceStart)
        {
            
        }

        public virtual void Consume(GameServiceEnd gameServiceEnd)
        {
            
        }

        public override void EventUpdate(EventArgs args)
        {
            Type type = args.GetType();
            var signature = new[] {type};
            MethodInfo methodInfo = GetType().GetMethod("Consume", signature);
            methodInfo.Invoke(this, new[] {args});
        }
    }
}