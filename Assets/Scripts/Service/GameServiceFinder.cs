using System;
using Interfaces;
using UnityEngine;

namespace Service
{
    public class GameServiceFinder : MonoBehaviour
    {
        public static GameServiceFinder Instance { get; private set; }
        [SerializeField] private GameService gameService;

        private void Awake()
        {
            Instance = this;
        }

        public void RegisterProducer(IProducer producer)
        {
            gameService.Subscribe(producer);
        }
    }
}