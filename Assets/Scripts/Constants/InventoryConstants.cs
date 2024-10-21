using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

public class InventoryConstants : MonoBehaviour
{
    [SerializeField] private Sprite _defaultInventoryItemIcon;
    [SerializeField] private Sprite _emptyInventoryItemIcon;
    [SerializeField] private string _defaultItemName;
    [SerializeField] private List<InventoryItem> _buildMenuItems;
    
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
        return _defaultItemName;
    }

    public List<InventoryItem> GetBuildMenuItems()
    {
        return _buildMenuItems;
    }
}
