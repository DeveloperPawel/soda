using System.Collections;
using System.Collections.Generic;
using Controllers;
using Interfaces;
using NUnit.Framework;
using Panels;
using Service;
using Service.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class UIControllerTests
{
    private GameObject UIController_GO;
    private GameObject panel_GO;
    private GameObject gameServiceFinder_GO;
    private ServiceFinder _gameServiceFinder;
    private UIController _uiController;
    private PanelMock _testPanel;
    private GameService _gameService;

    public class PanelMock : Panel
    {
        protected override void Awake()
        {
            base.Awake();
            eventArg = new GameServiceStart();
        }

        protected override void Start()
        {
            UIControllerMock.Instance.Register(eventArg.GetType(), this);
            SubscribeGameServiceFinder();
        }

        protected override void RegisterProducer()
        {
            
        }
    }
    public class UIControllerMock : UIController
    {
        public Dictionary<System.Type, List<GameObject>> panelDictionary => typePanelDictionary;

        protected override void Start()
        {
            //do nothing
        }
        
        public void AddGameService(GameService gameService)
        {
            this.gameService = gameService;
            Subscribe(gameService);
        }
    }

    public class GameServiceFinderMock : GameServiceFinder
    {
        // public void RegisterProducer(IProducer producer)
        // {
        //     
        // }
        
        public void AddGameService(GameService gameService)
        {
            this.gameService = gameService;
        }
    }

    [OneTimeSetUp]
    public void OnTimeSetup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Tests/Scenes/UIContollerTest.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }
    
    [SetUp]
    public void Setup()
    {
        _gameService = ScriptableObject.CreateInstance<GameServiceTests.GameServiceMock>();
        
        gameServiceFinder_GO = GameObject.Instantiate(new GameObject("gameServiceFinder"));
        _gameServiceFinder = gameServiceFinder_GO.AddComponent<GameServiceFinderMock>();
        (_gameServiceFinder as GameServiceFinderMock).AddGameService(_gameService);
        
        UIController_GO = GameObject.Instantiate(new GameObject("uiController"));
        _uiController = UIController_GO.AddComponent<UIControllerMock>();
        (_uiController as UIControllerMock).AddGameService(_gameService);

        panel_GO = GameObject.Instantiate(new GameObject("panel"));
        _testPanel = panel_GO.AddComponent<PanelMock>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(UIController_GO);
        Object.Destroy(panel_GO);
        Object.Destroy(_gameService);
        Object.Destroy(gameServiceFinder_GO);
    }

    [UnityTest]
    public IEnumerator UIController_PlayMode_RegisterProducer()
    {
        _gameService = ScriptableObject.CreateInstance<GameServiceTests.GameServiceMock>();
        
        gameServiceFinder_GO = GameObject.Instantiate(new GameObject("gameServiceFinder"));
        _gameServiceFinder = gameServiceFinder_GO.AddComponent<GameServiceFinderMock>();
        (_gameServiceFinder as GameServiceFinderMock).AddGameService(_gameService);
        
        UIController_GO = GameObject.Instantiate(new GameObject("uiController"));
        _uiController = UIController_GO.AddComponent<UIControllerMock>();
        (_uiController as UIControllerMock).AddGameService(_gameService);
        
        panel_GO = GameObject.Instantiate(new GameObject("npanel"));
        _testPanel = panel_GO.AddComponent<PanelMock>();
        yield return null;
        _uiController.Register((new GameServiceStart()).GetType(), _testPanel);
        yield return null;
        Assert.AreEqual(1, (_uiController as UIControllerMock).panelDictionary.Count);
    }
    
    [UnityTest]
    public IEnumerator UIController_PlayMode_OnEventDeactivateProducerGO()
    {
        _gameService = ScriptableObject.CreateInstance<GameServiceTests.GameServiceMock>();
        
        gameServiceFinder_GO = GameObject.Instantiate(new GameObject("gameServiceFinder"));
        _gameServiceFinder = gameServiceFinder_GO.AddComponent<GameServiceFinderMock>();
        (_gameServiceFinder as GameServiceFinderMock).AddGameService(_gameService);
        
        UIController_GO = GameObject.Instantiate(new GameObject("uiController"));
        _uiController = UIController_GO.AddComponent<UIControllerMock>();
        (_uiController as UIControllerMock).AddGameService(_gameService);
        
        panel_GO = GameObject.Instantiate(new GameObject("npanel"));
        _testPanel = panel_GO.AddComponent<PanelMock>();
        yield return null;
        _uiController.Register((new GameServiceStart()).GetType(), _testPanel);
        yield return null;
        _uiController.Consume(new GameServiceEnd());
        yield return null;
        Assert.AreEqual(false, panel_GO.activeSelf);
    }
}
