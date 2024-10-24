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
        if(_buildMenu != null) //If the build menu object has already been cached
        {
            _buildMenu.gameObject.SetActive(true);
        }
        else //Find the build menu object and cache it
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                if (_buildMenu == null) //Cache the first (should be the only) build menu found
                {
                    rootObjects[i].TryGetComponent(out _buildMenu);
                }
            }
        }

        if (_buildMenu == null) //uh oh, no build menu
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
