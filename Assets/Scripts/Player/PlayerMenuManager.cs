using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMenuManager : InputReviever
{
    private Manager_MenuManager _menuManager;    

    [Inject]
    private void Initialize(Manager_MenuManager menuManager)
    {
        _menuManager = menuManager;
    }

    protected override void ListenForInput()
    {
        _input.OnOpenBuildMenu += OpenBuildMenu;
        _input.OnCloseMenu += CloseBuildMenu;
    }

    protected override void UnlistenForInput()
    {
        _input.OnOpenBuildMenu -= OpenBuildMenu;
        _input.OnCloseMenu -= CloseBuildMenu;
    }

    private void OpenBuildMenu()
    {
        _menuManager.OpenBuildMenu();
    }

    private void CloseBuildMenu()
    {
        _menuManager.CloseBuildMenu();
    }
}
