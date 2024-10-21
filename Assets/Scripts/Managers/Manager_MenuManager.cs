using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using Zenject;

public class Manager_MenuManager : MonoBehaviour
{
    private Manager_SceneManager _sceneManager;
    private Manager_InputManager _InputManager;

    //Menus
    private Menu_BuildMenu _buildMenu;

    [Inject]
    private void Initailize(Manager_SceneManager sceneManager, Manager_InputManager inputManager)
    {
        _sceneManager = sceneManager;
        _InputManager = inputManager;
    }

    public void OpenBuildMenu()
    {
        _sceneManager.LoadScene(SceneConstants.Scenes.BuildMenu, OnBuildMenuLoaded);
    }

    private void OnBuildMenuLoaded(Scene scene)
    {
        if(_buildMenu != null)
        {
            Debug.Log("Quick load");

            _buildMenu.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("slow load");

            GameObject[] rootObjects = scene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                if (_buildMenu == null)
                {
                    rootObjects[i].TryGetComponent(out _buildMenu);
                }
            }
        }

        if (_buildMenu == null)
        {
            Debug.LogWarning("Failed to load build menu!");
        }
        else
        {
            MenuOpened();
        }
    }

    public void CloseBuildMenu()
    {
        if (_buildMenu == null) return;

        _buildMenu.gameObject.SetActive(false);
        MenuClosed();
    }

    private void MenuOpened()
    {
        _InputManager.SwitchInput("Menus");
    }

    private void MenuClosed()
    {
        _InputManager.SwitchInput("Player");
    }
}
