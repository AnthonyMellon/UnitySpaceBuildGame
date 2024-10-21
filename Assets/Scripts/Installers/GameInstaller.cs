using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameConstants _gameConstants;
    
    public override void InstallBindings()
    {
        Container.Bind<GameConstants>().FromComponentInNewPrefab(_gameConstants).AsSingle();
    }
}