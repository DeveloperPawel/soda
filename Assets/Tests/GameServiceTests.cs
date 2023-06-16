using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Service;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameServiceTests
{
    private ScriptableObject SO;

    [OneTimeSetUp]
    public void OnTimeSetup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Tests/Scenes/GameServiceTest.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }
    
    [SetUp]
    public void Setup()
    {
        SO = ScriptableObject.CreateInstance<GameService>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(SO);
    }
    
    [UnityTest]
    public IEnumerator GameService_Playmode_OnCreateGameService()
    {
        yield return null;
        Assert.IsNotNull(Object.FindObjectOfType<GameService>());
    }
}
