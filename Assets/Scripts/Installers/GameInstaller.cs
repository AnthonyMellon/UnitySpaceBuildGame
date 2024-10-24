using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    //Managers
    [SerializeField] private Manager_GameManager _gameManager;
    public override void InstallBindings()
    {
        //Managers
        Container.Bind<Manager_GameManager>().FromComponentInNewPrefab(_gameManager).AsSingle();
    }
}