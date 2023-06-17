using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Service;
using Service.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class GameServiceTests
{
    private GameServiceMock gameService;
    
    public class GameServiceMock : GameService
    {
        public int numTimesCalled = 0;
        protected override void OnEnable()
        {
            _event = new UnityEvent<EventArgs>();
            isInitialized = true;
        }

        public void CallGameStart()
        {
            numTimesCalled++;
            _event?.Invoke(new GameServiceStart());
        }
    }
    
    [OneTimeSetUp]
    public void OnTimeSetup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Tests/Scenes/GameServiceTest.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }
    
    [SetUp]
    public void Setup()
    {
        gameService = ScriptableObject.CreateInstance<GameServiceMock>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(gameService);
    }
    
    [UnityTest]
    public IEnumerator GameService_Playmode_OnCreateGameService()
    {
        yield return null;
        Assert.IsNotNull(Object.FindObjectOfType<GameService>());
    }
    
    [UnityTest]
    public IEnumerator GameService_Playmode_IsInitialized()
    {
        yield return null;
        Assert.AreEqual(true, gameService.Initialized);
    }
    
    [UnityTest]
    public IEnumerator GameService_Playmode_OnFireEvent()
    {
        bool flag = false;
        
        yield return null;
        gameService.Event.AddListener((eventArgs) =>
        {
            flag = true;
        });
        
        gameService.CallGameStart();

        yield return null;
        Assert.AreEqual(true, flag);
    }
}
