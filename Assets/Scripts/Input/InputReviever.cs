using UnityEngine;
using Zenject;

public abstract class InputReviever : MonoBehaviour
{
    protected Input_InputProvider _input;

    [Inject]
    private void Initialize(Input_InputProvider input)
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
