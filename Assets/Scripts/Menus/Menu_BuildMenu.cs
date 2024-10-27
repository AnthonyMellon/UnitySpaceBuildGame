using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Menu_BuildMenu : InputReviever
{    
    //Serialized
    [SerializeField] private RectTransform _slotContainer;
    [SerializeField] private List<InventoryItem> _menuItems;

    //Injected
    private InventorySlot.Factory _inventorySlotFactory;

    //Other
    private List<InventorySlot> _slots = new List<InventorySlot>();
    private InventorySlot _selectedSlot;
    private Hotbar _owner;

    [Inject]
    private void Initialize(InventorySlot.Factory inventorySlotFactory)
    {
        _inventorySlotFactory = inventorySlotFactory;
    }

    protected override void ListenForInput()
    {
        _input.OnMenuNumberPressed += OnNumberPressed;
    }
    protected override void UnlistenForInput()
    {
        _input.OnMenuNumberPressed -= OnNumberPressed;
    }

    public void OpenMenu(Hotbar owner)
    {
        gameObject.SetActive(true);

        _owner = owner;
        CreateSlotsFor(_menuItems);
    }

    public void CloseMenu()
    {
        DestroySlots();

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Create all slots needed to display <paramref name="inventoryItems"/>
    /// </summary>
    /// <param name="inventoryItems">The items to be displayed</param>
    private void CreateSlotsFor(List<InventoryItem> inventoryItems)
    {
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            InventorySlot slot = _inventorySlotFactory.Create(inventoryItems[i]);
            slot.transform.SetParent(_slotContainer, false);
            slot.OnSlotSelectChanged += OnSlotSelectChanged;
            _slots.Add(slot);
        }
    }

    /// <summary>
    /// Destroy all slots
    /// </summary>
    private void DestroySlots()
    {
        for(int i = 0; i <  _slots.Count; i++)
        {
            _slots[i].Destroy();
        }
        _slots.Clear();
    }

    private void OnSlotSelectChanged(bool selected, InventorySlot slot)
    {
        if (selected) _selectedSlot = slot;
        else _selectedSlot = null;
    }

    private void OnNumberPressed(int num)
    {
        int slotIndex = num - 1;
        SetHotbarSlotDataToSelectedSlotData(slotIndex);
    }

    private void SetHotbarSlotDataToSelectedSlotData(int slotIndex)
    {
        if (_owner == null) return;
        if(_selectedSlot == null) return;

        InventoryItem data = _selectedSlot.GetData();
        _owner.SetSlotData(slotIndex, data);
    }
}
