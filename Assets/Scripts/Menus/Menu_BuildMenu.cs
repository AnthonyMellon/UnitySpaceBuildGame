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
        Debug.Log("Open");
        CreateSlots();
    }

    protected override void CloseMenu()
    {
        Debug.Log("Close");
        DestroySlots();
    }

    private void CreateSlots()
    {
        for(int i = 0; i < _menuItems.Count; i++)
        {
            InventorySlot slot = _inventorySlotFactory.Create(_menuItems[i]);
            slot.transform.SetParent(_slotContainer, false);
            _slots.Add(slot);

        }
    }

    private void DestroySlots()
    {
        for(int i = 0; i <  _slots.Count; i++)
        {
            _slots[i].Destroy();
        }
        _slots.Clear();
    }
}
