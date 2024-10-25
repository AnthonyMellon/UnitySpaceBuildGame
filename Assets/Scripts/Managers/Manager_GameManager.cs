using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Manager_GameManager : MonoBehaviour
{
    public delegate void GameStartHandler();
    public GameStartHandler OnGameStart;

    private Input_InputProvider _inputProvider;

    [Inject]
    private void Initialize(Input_InputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }

    private void Start()
    {
        //Ensure correct input mode
        _inputProvider.SetInputMode(Input_InputProvider.InputModes.Game);

        //Tell anyone else who wants to know the the game has started!
        OnGameStart?.Invoke();
    }
}
