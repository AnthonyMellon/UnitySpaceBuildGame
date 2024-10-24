using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _nameTag;
    [SerializeField] private InventoryItem _itemData;

    private InventoryConstants _inventoryConstants;

    [Inject]
    private void Initialise([InjectOptional]InventoryItem data, InventoryConstants inventoryConstants)
    {
        _inventoryConstants = inventoryConstants;

        if(data != null)
        {
            _itemData = data;
        }
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

    /// <summary>
    /// Enable / disable the highlight
    /// </summary>
    /// <param name="enable">enable highlight?</param>
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
        

        if (_itemData == null) // no item in slot
        {
            newIcon = _inventoryConstants.GetEmptyInventoryItemIcon();
        }
        else // get icon from item data
        {
            newIcon = _itemData.GetIcon();
        }
        
        // set srpite to newIcon. If null, fallback to default icon
        _iconImage.sprite = newIcon != null ?
            newIcon :
            _inventoryConstants.GetDefaultInventoryItemIcon();
    }

    private void UpdateNameTag()
    {
        if (_nameTag == null) return;

        string newName = null;

        //Ensure there is data to read from
        if(_itemData != null)
        {
            newName = _itemData.GetName();

            //IF the data has no name, fallback to the default
            if (string.IsNullOrEmpty(newName)) newName = _inventoryConstants.GetDefaultItemName();
        }

        _nameTag.text = newName;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<InventoryItem, InventorySlot> { };
}
