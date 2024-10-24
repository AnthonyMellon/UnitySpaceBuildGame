using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_GameManager : MonoBehaviour
{
    public delegate void GameStartHandler();
    public GameStartHandler OnGameStart;

    private void Start()
    {
        OnGameStart?.Invoke();
    }
}
