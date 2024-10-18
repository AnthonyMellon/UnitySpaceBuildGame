using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PlayerInputReciever : MonoBehaviour
{
    [SerializeField] protected PlayerInputTranslater _input;

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
