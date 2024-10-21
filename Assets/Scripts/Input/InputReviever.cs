using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using Zenject;

public abstract class InputReviever : MonoBehaviour
{
    protected Manager_InputManager _input;

    [Inject]
    private void Initialize(Manager_InputManager input)
    {
        _input = input;
    }

    protected void OnEnable()
    {
        ListenForInput();
    }

    protected void OnDisable()
    {
        UnlistenForInput();
    }

    protected abstract void ListenForInput();
    protected abstract void UnlistenForInput();

}
