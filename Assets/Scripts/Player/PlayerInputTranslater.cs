using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerInputTranslater : MonoBehaviour
{
    #region Event Decleration
    public delegate void ChangeDirectionHandler (Vector3 direction);
    public event ChangeDirectionHandler OnChangeDirection;

    public delegate void JumpHandler();
    public event JumpHandler OnJump;

    public delegate void VerticalScrollHandler(int direction);
    public event VerticalScrollHandler OnVerticalScroll;

    public delegate void NumberPressedHandler(int num);
    public event NumberPressedHandler OnNumberPressed;

    public delegate void HorizontalMouseMoveHandler(float delta);
    public event HorizontalMouseMoveHandler OnHorizontalMouseMove;

    public delegate void VerticalMouseMoveHandler(float delta);
    public event VerticalMouseMoveHandler OnVerticalMouseMove;

    public delegate void SprintToggleHandler(bool toggle);
    public event SprintToggleHandler OnSprintToggle;
    #endregion

    public Vector2 MovementDirection { get; private set; }

    public void ChangeDirection(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        OnChangeDirection?.Invoke(new Vector3(direction.x, 0, direction.y));
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton())
        {
            OnJump?.Invoke();
        }
    }

    public void Scroll(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        //Only trigger on press, not on release
        if(context.phase == InputActionPhase.Started)
        {
            int verticalDirection = (int)direction.y;
            OnVerticalScroll?.Invoke(verticalDirection);
        }
    }

    public void Number(InputAction.CallbackContext context)
    {
        int num = (int)context.ReadValue<float>();

        if(context.phase == InputActionPhase.Started)
        {
            OnNumberPressed?.Invoke(num);
        }

    }

    public void MouseMove(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();

        if(mouseDelta != null)
        {
            OnHorizontalMouseMove?.Invoke(mouseDelta.x);
            OnVerticalMouseMove?.Invoke(mouseDelta.y);
        }
    }

    public void Spint(InputAction.CallbackContext context)
    {
        bool toggle = context.ReadValueAsButton();
        OnSprintToggle?.Invoke(toggle);
    }
}
