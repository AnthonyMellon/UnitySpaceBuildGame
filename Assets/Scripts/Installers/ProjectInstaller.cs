using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private InventorySlot _buildMenuSlot;

    //Constants
    [Header("Constants")]
    [SerializeField] private InventoryConstants _inventoryConstants;

    //Managers
    [Header("Managers")]
    [SerializeField] private Manager_SceneManager _sceneManager;
    [SerializeField] private Manager_MenuManager _menuManager;
    [SerializeField] private Manager_InputManager _inputManager;

/*    //Menus
    [Header("Menus")]
    [SerializeField] private Menu_BuildMenu _buildMenu;*/

    public override void InstallBindings()
    {
        //Constants
        Container.Bind<InventoryConstants>().FromComponentInNewPrefab(_inventoryConstants).AsSingle();

        //Managers
        Container.Bind<Manager_SceneManager>().FromComponentInNewPrefab(_sceneManager).AsSingle();
        Container.Bind<Manager_MenuManager>().FromComponentInNewPrefab(_menuManager).AsSingle();
        Container.Bind<Manager_InputManager>().FromComponentInNewPrefab(_inputManager).AsSingle();

        //Menus
        //Container.Bind<Menu_BuildMenu>().FromComponentInNewPrefab(_buildMenu).AsSingle();

        //Factories
        Container.BindFactory<InventoryItem, InventorySlot, InventorySlot.Factory>().FromComponentInNewPrefab(_buildMenuSlot).AsSingle();
    }
}