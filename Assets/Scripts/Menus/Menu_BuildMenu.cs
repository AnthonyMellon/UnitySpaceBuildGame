using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Menu_BuildMenu : Menu_MenuBase
{    
    //Serialized
    [SerializeField] private RectTransform _slotContainer;
    [SerializeField] private List<InventoryItem> _menuItems;

    //Injected
    private InventorySlot.Factory _inventorySlotFactory;

    //Other
    private List<InventorySlot> _slots = new List<InventorySlot>();

    [Inject]
    private void Initialize(InventorySlot.Factory inventorySlotFactory)
    {
        _inventorySlotFactory = inventorySlotFactory;
    }

    protected override void OpenMenu()
    {
        CreateSlotsFor(_menuItems);
    }

    protected override void CloseMenu()
    {
        DestroySlots();
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
}
