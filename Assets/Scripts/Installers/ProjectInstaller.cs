using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private BuildMenuSlot _buildMenuSlot;

    //Constants
    [Header("Constants")]
    [SerializeField] private InventoryConstants _inventoryConstants;

    //Managers
    [Header("Managers")]
    [SerializeField] private Manager_SceneManager _sceneManager;
    [SerializeField] private Manager_MenuManager _menuManager;
    [SerializeField] private Input_InputProvider _inputManager;

    public override void InstallBindings()
    {
        //Constants
        Container.Bind<InventoryConstants>().FromComponentInNewPrefab(_inventoryConstants).AsSingle();

        //Managers
        Container.Bind<Manager_SceneManager>().FromComponentInNewPrefab(_sceneManager).AsSingle();
        Container.Bind<Manager_MenuManager>().FromComponentInNewPrefab(_menuManager).AsSingle();
        Container.Bind<Input_InputProvider>().FromComponentInNewPrefab(_inputManager).AsSingle();

        //Factories
        Container.BindFactory<InventoryItem, InventorySlot, InventorySlot.Factory>().FromComponentInNewPrefab(_buildMenuSlot).AsSingle();
    }
}