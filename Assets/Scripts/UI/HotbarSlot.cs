using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HotbarSlot : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _nameTag;
    [SerializeField] private InventoryItem itemData;

    private InventoryConstants _inventoryConstants;

    [Inject]
    private void Initialise(InventoryConstants inventoryConstants)
    {
        _inventoryConstants = inventoryConstants;
    }

    private void OnEnable()
    {
        OnDataChange();
    }

    private void OnDataChange()
    {
        UpdateIcon();
        UpdateNameTag();
    }

    public void Highlight(bool enable)
    {
        if (enable) EnableHighlight();
        else DisableHighlight();
    }

    private void EnableHighlight()
    {
        _backgroundImage.color = _highlightColor;
    }

    private void DisableHighlight()
    {
        _backgroundImage.color = _defaultColor;
    }

    private void UpdateIcon()
    {
        Sprite newIcon = null;
        

        if (itemData == null) // no item in slot
        {
            newIcon = _inventoryConstants.GetEmptyInventoryItemIcon();
        }
        else // get icon from item data
        {
            newIcon = itemData.GetIcon();
        }
        
        // set srpite to newIcon. If null, fallback to default icon
        _iconImage.sprite = newIcon != null ?
            newIcon :
            _inventoryConstants.GetDefaultInventoryItemIcon();
    }

    private void UpdateNameTag()
    {
        string newName = null;

        if(itemData != null)
        {
            newName = itemData.GetName();

            if (string.IsNullOrEmpty(newName)) newName = _inventoryConstants.GetDefaultItemName();
        }

        _nameTag.text = newName;
    }
}
