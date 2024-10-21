using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu_MenuBase : MonoBehaviour
{
    protected abstract void OpenMenu();
    protected abstract void CloseMenu();

    private void OnEnable()
    {
        OpenMenu();
    }

    private void OnDisable()
    {
        CloseMenu();
    }
}
