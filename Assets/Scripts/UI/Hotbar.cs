using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    [SerializeField] private GameObject _slotsContainer;
    private List<HotbarSlot> _slots;
    private int _selectedSlotIndex = 0;

    private void OnEnable()
    {
        UpdateAllSlotHighlights();
    }
    
    /// <summary>
    /// Gets and stores all slots
    /// </summary>
    private void GetAllSlots()
    {
        _slots?.Clear();
        _slots = _slotsContainer.GetComponentsInChildren<HotbarSlot>().ToList();
    }

    /// <summary>
    /// Ensures all slots are highlighted correctly
    /// </summary>
    private void UpdateAllSlotHighlights()
    {
        if (_slots == null) GetAllSlots();

        for(int i = 0; i < _slots.Count; i++)
        {
            _slots[i].Highlight(i == _selectedSlotIndex);
        }
    }

    /// <summary>
    /// Scroll to next or previous slot
    /// </summary>
    /// <param name="direction">negative for prevois slots, positive for next slots</param>
    public void ScrollSelectedSlot(int direction)
    {
        if (direction == 0) return;
        if (_slots == null) GetAllSlots();
        if (_slots.Count == 0) return;

        int newSlot = (_selectedSlotIndex + direction);

        //Wrap slot index
        if (newSlot < 0) newSlot = _slots.Count - 1;
        else if (newSlot >= _slots.Count) newSlot = 0;

        SetSelectedSlot(newSlot);
    }

    /// <summary>
    /// Select new slot
    /// </summary>
    /// <param name="newSlot">Slot index to select</param>
    public void SetSelectedSlot(int newSlot)
    {
        //Update highlights
        HighlightSlot(_selectedSlotIndex, false);
        HighlightSlot(newSlot, true);

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

        _slots[slotIndex].Highlight(highlight);
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

}
