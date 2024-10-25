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
    private Input_InputProvider _InputManager;

    //Menus
    private Menu_BuildMenu _buildMenu;

    [Inject]
    private void Initailize(Manager_SceneManager sceneManager, Input_InputProvider inputManager)
    {
        _sceneManager = sceneManager;
        _InputManager = inputManager;
    }

    public void OpenBuildMenu(Hotbar owner)
    {
        _sceneManager.LoadScene(SceneConstants.Scenes.BuildMenu, (Scene scene) => { OnBuildMenuLoaded(scene, owner); });
    }

    private void OnBuildMenuLoaded(Scene scene, Hotbar owner)
    {
        if(_buildMenu == null) //If the build menu has not already been loaded, load and cache it
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
            _buildMenu.OpenMenu(owner);
            MenuOpened();
        }
    }

    public void CloseBuildMenu()
    {
        if (_buildMenu == null) return;

        _buildMenu.CloseMenu();
        MenuClosed();
    }

    private void MenuOpened()
    {
        _InputManager.SetInputMode(Input_InputProvider.InputModes.Menus);
    }

    private void MenuClosed()
    {
        _InputManager.SetInputMode(Input_InputProvider.InputModes.Game);
    }
}
