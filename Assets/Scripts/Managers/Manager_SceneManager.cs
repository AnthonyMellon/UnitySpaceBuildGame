using ModestTree.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_SceneManager : MonoBehaviour
{
    /// <summary>
    /// Attempts to load a given scene
    /// </summary>
    /// <param name="sceneName">The name of the scene to load</param>
    /// <returns>True if the scene gets loaded. False if the scene is already loaded</returns>
    public void LoadScene(SceneConstants.Scenes scene, Action<Scene> onLoadComplete)
    {
        string sceneName = SceneConstants.SceneNames[scene];
        Scene loadedScene;

        //Scene already loaded?
        loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene.name != null)
        {
            onLoadComplete(loadedScene);
            return;
        }

        //Load scene        
        StartCoroutine(LoadSceneEnum(sceneName, onLoadComplete));
    }

    private IEnumerator LoadSceneEnum(string sceneName, Action<Scene> onLoadComplete)
    {
        //Begin scene load
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        //Wait until scene is loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //Done :)
        OnSceneLoaded(sceneName, onLoadComplete);
    }

    private void OnSceneLoaded(string sceneName, Action<Scene> onLoadComplete)
    {
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        onLoadComplete.Invoke(loadedScene);
    }

    public void UnloadScene(SceneConstants.Scenes scene)
    {
        string sceneName = SceneConstants.SceneNames[scene];

        if (SceneManager.GetSceneByName(sceneName).name != null)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
