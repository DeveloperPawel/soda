using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Controllers;
using Interfaces;
using NUnit.Framework;
using Service;
using Service.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class GameControllerTests
{
    private GameObject go;
    private GameControllerMock _gameController;
    private GameServiceTests.GameServiceMock _gameService;
    
    public class GameControllerMock : GameController
    {
        public int numTimesCalled = 0;
        public int eventT = 0;
        public int numEndConsumeCalled = 0;
        
        protected override void OnEnable()
        {
            //do nothing
        }
        
        public override void EventUpdate(EventArgs args)
        {
            base.EventUpdate(args);
            eventT++;
        }
        
        public override void Consume(GameServiceStart gameServiceStart)
        {
            numTimesCalled++;
        }
        
        public virtual void Consume(GameServiceEnd gameServiceEnd)
        {
            numEndConsumeCalled++;
        }

        public void AddGameService(GameService gameService)
        {
            this.gameService = gameService;
            Subscribe(gameService);
        }
    }
    
    [OneTimeSetUp]
    public void OnTimeSetup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Tests/Scenes/GameControllerTest.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }
    
    [SetUp]
    public void Setup()
    {
        go = new GameObject("control");
        _gameController = go.AddComponent<GameControllerMock>();
        _gameService = ScriptableObject.CreateInstance<GameServiceTests.GameServiceMock>();
        _gameController.AddGameService(_gameService);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_gameService);
        Object.Destroy(go);
    }
    
    [UnityTest]
    public IEnumerator GameController_OnCreateGameController()
    {
        yield return null;
        Assert.IsNotNull(_gameService);
        Assert.IsNotNull(_gameController);
    }

    [UnityTest]
    public IEnumerator GameController_OnGameControllerEventUpdate_GameStart()
    {
        yield return null;
        GameServiceStart gameServiceStart = new GameServiceStart();
        _gameController.EventUpdate(gameServiceStart);
        Assert.AreEqual(1, _gameController.eventT);
        Assert.AreEqual(1, _gameController.numTimesCalled);
    }
    
    [UnityTest]
    public IEnumerator GameController_OnGameControllerEventUpdate_GameEnd()
    {
        yield return null;
        GameServiceEnd gameServiceEnd = new GameServiceEnd();
        _gameController.EventUpdate(gameServiceEnd);
        Assert.AreEqual(1, _gameController.eventT);
        Assert.AreEqual(1, _gameController.numEndConsumeCalled);
    }
}
