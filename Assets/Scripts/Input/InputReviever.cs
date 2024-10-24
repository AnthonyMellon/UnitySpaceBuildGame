using UnityEngine;
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

    /// <summary>
    /// Subsribe to input events
    /// </summary>
    protected abstract void ListenForInput();

    /// <summary>
    /// Unsubscribe from input events
    /// </summary>
    protected abstract void UnlistenForInput();

}
