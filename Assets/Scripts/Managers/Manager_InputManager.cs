using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Manager_InputManager : MonoBehaviour
{
   [SerializeField] private PlayerInput _pInput;

    #region Event Decleration
    public delegate void MoveHandler (Vector3 direction);
    public event MoveHandler OnMove;

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

    public delegate void OpenBuildMenuHandler();
    public event OpenBuildMenuHandler OnOpenBuildMenu;

    public delegate void CloseMenuHandler();
    public event CloseMenuHandler OnCloseMenu;
    #endregion

    #region Player
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        OnMove?.Invoke(new Vector3(direction.x, 0, direction.y));
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started) //Only trigger on initial press
        {
            OnJump?.Invoke();
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        bool toggle = context.ReadValueAsButton();
        OnSprintToggle?.Invoke(toggle);
    }
    #endregion

    #region Menus

    public void CloseMenu(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started) //Only trigger on initial press
        {
            OnCloseMenu.Invoke();
        }
    }

    public void OpenBuildMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) //Only trigger on initial press
        {
            OnOpenBuildMenu?.Invoke();
        }
    }
    #endregion

    #region Generic
    public void MouseScroll(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        
        if (context.phase == InputActionPhase.Started) //Only trigger on initial press
        {
            //Vertical scroll
            int verticalDirection = (int)direction.y;
            OnVerticalScroll?.Invoke(verticalDirection);

            //Horizontal scroll can go here if ever needed
        }
    }

    public void NumberPressed(InputAction.CallbackContext context)
    {
        int num = (int)context.ReadValue<float>();

        if (context.phase == InputActionPhase.Started) //Only trigger on initial press
        {
            OnNumberPressed?.Invoke(num);
        }
    }

    public void MouseMove(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();

        OnHorizontalMouseMove?.Invoke(mouseDelta.x);
        OnVerticalMouseMove?.Invoke(mouseDelta.y);
    }
    #endregion


    public void SwitchInput(string InputName) //swap this to a callback?
    {
        _pInput.SwitchCurrentActionMap(InputName);
    }
}
