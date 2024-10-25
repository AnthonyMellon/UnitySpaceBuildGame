using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Hotbar : InputReviever
{
    [SerializeField] private GameObject _slotsContainer;
    private List<InventorySlot> _slots;
    private int _selectedSlotIndex = 0;

    private Manager_MenuManager _menuManager;

    [Inject]
    private void Initialize(Manager_MenuManager menuManager)
    {
        _menuManager = menuManager;
    }

    protected new void OnEnable()
    {
        base.OnEnable();

        UpdateAllSlotHighlights();
    }

    protected override void ListenForInput()
    {
        _input.OnOpenBuildMenu += OpenBuildMenu;
        _input.OnCloseMenu += CloseBuildMenu;
    }

    protected override void UnlistenForInput()
    {
        _input.OnOpenBuildMenu -= OpenBuildMenu;
        _input.OnCloseMenu += CloseBuildMenu;
    }

    /// <summary>
    /// Gets and stores all slots
    /// </summary>
    private void GetAllSlots()
    {
        _slots?.Clear();
        _slots = _slotsContainer.GetComponentsInChildren<InventorySlot>().ToList();
    }

    /// <summary>
    /// Ensures all slots are highlighted correctly
    /// </summary>
    private void UpdateAllSlotHighlights()
    {
        if (_slots == null) GetAllSlots();

        for(int i = 0; i < _slots.Count; i++)
        {
            _slots[i].Select(i == _selectedSlotIndex);
        }
    }

    /// <summary>
    /// Scroll to next or previous slot
    /// </summary>
    /// <param name="direction">negative for previous slots, positive for next slots</param>
    public void ScrollSelectedSlot(int direction)
    {
        if (direction == 0) return;
        if (_slots == null) GetAllSlots();
        if (_slots.Count == 0) return;

        int newSlotIndex = (_selectedSlotIndex + direction);

        //Wrap slot index
        if (newSlotIndex < 0) newSlotIndex = _slots.Count - 1;
        else if (newSlotIndex >= _slots.Count) newSlotIndex = 0;

        SetSelectedSlot(newSlotIndex);
    }

    /// <summary>
    /// Select new slot
    /// </summary>
    /// <param name="newSlot">Slot index to select</param>
    public void SetSelectedSlot(int newSlot)
    {
        //Update slots selection status
        _slots[_selectedSlotIndex].Select(false);
        _slots[newSlot].Select(true);

        _selectedSlotIndex = newSlot;
    }

    /// <summary>
    /// Highlights given slot, does nothing if slot is invalid
    /// </summary>
    /// <param name="slotIndex">Slot index to hihglight</param>
    /// <param name="highlight">true / flase - highlight on / off</param>
    private void HighlightSlot(int slotIndex, bool highlight)
    {
        if (!IsValidSlotIndex(slotIndex)) return;

        _slots[slotIndex].Select(highlight);
    }

    /// <summary>
    /// Returns if given slot is valid
    /// </summary>
    /// <param name="slotIndex">Slot index to check validity of</param>
    /// <returns>Is slot valid?</returns>
    private bool IsValidSlotIndex(int slotIndex)
    {
        if (slotIndex < 0) return false;
        if (_slots == null) return false;
        if (slotIndex >= _slots.Count) return false;

        return true;
    }

    /// <summary>
    /// Set a slots data
    /// </summary>
    /// <param name="slotNum">The slot to set data for</param>
    /// <param name="data">The data the slot should accept</param>
    public void SetSlotData(int slotNum, InventoryItem data)
    {
        if (slotNum < 0 || slotNum >= _slots.Count) return; //Invalid slot num

        _slots[slotNum].SetData(data);
    }

    private void OpenBuildMenu()
    {
        _menuManager.OpenBuildMenu(this);
    }

    private void CloseBuildMenu()
    {
        _menuManager.CloseBuildMenu();
    }

}
