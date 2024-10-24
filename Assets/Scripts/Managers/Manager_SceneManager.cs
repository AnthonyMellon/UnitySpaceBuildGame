using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_SceneManager : MonoBehaviour
{
    /// <summary>
    /// Attempts to load a given scene
    /// </summary>
    /// <param name="scene">The scene to load</param>
    /// <param name="onLoadComplete">The action to be called once the scene is laoded</param>
    /// <returns></returns>
    public void LoadScene(SceneConstants.Scenes scene, Action<Scene> onLoadComplete)
    {
        string sceneName = SceneConstants.SceneNames[scene];
        Scene loadedScene;

        //Check if the scene is already loaded. If it is then loading is complete
        loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene.name != null)
        {
            onLoadComplete(loadedScene);
        }
        else
        {
            //Start loading the scene
            StartCoroutine(LoadSceneEnum(sceneName, onLoadComplete));
        }


    }

    /// <summary>
    /// Load a given scene in the background
    /// </summary>
    /// <param name="sceneName">The scene to be loaded</param>
    /// <param name="onLoadComplete">Acion to be called once the scene is loaded</param>
    /// <returns></returns>
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

    /// <summary>
    /// Unload a given scene
    /// </summary>
    /// <param name="scene">The scene to unload</param>
    public void UnloadScene(SceneConstants.Scenes scene)
    {
        string sceneName = SceneConstants.SceneNames[scene];

        if (SceneManager.GetSceneByName(sceneName).name != null)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
