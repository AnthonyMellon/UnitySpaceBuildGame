using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class InventoryConstants : MonoBehaviour
{
    [SerializeField] private Sprite _defaultInventoryItemIcon;
    [SerializeField] private Sprite _emptyInventoryItemIcon;
    [SerializeField] private string _defaultItemName;

    public Sprite GetDefaultInventoryItemIcon()
    {
        return _defaultInventoryItemIcon;
    }

    public Sprite GetEmptyInventoryItemIcon()
    {
        return _emptyInventoryItemIcon;
    }

    public string GetDefaultItemName()
    {
        Debug.Log("Getting default item name");

        return _defaultItemName;
    }
}
