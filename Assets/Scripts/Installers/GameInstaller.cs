using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameConstants _gameConstants;
    [SerializeField] private InventoryConstants _inventoryConstants;
    
    public override void InstallBindings()
    {
        Container.Bind<GameConstants>().FromComponentInNewPrefab(_gameConstants).AsSingle();
        Container.Bind<InventoryConstants>().FromComponentInNewPrefab(_inventoryConstants).AsSingle();
    }
}