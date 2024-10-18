using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : PlayerInputReciever
{
    [SerializeField] private Hotbar _hotbar;    

    #region Event Subscription
    protected override void ListenForInput()
    {
        _input.OnVerticalScroll += OnScroll;
        _input.OnNumberPressed += OnNumberPressed;
    }

    protected override void UnlistenForInput()
    {
        _input.OnVerticalScroll -= OnScroll;
        _input.OnNumberPressed -= OnNumberPressed;
    }
    #endregion

    private void OnScroll(int direction)
    {
        _hotbar.ScrollSelectedSlot(direction);
    }

    private void OnNumberPressed(int num)
    {
        //eg. 1 key should select slot number 0
        int slotIndex = num - 1;

        _hotbar.SetSelectedSlot(slotIndex);
    }
}
