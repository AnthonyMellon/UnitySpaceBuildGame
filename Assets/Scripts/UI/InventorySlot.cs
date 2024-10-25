using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour
{
    //Serialized
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _nameTag;
    [SerializeField] private InventoryItem _itemData;

    //Injected
    private InventoryConstants _inventoryConstants;

    //Events
    public delegate void SlotSelectChangeHandler(bool selected, InventorySlot slot);
    public SlotSelectChangeHandler OnSlotSelectChanged;


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
        Select(false);
    }

    private void OnDataChange()
    {
        UpdateIcon();
        UpdateNameTag();
    }

    /// <summary>
    /// Toggle weather the slot is in a selected state
    /// </summary>
    /// <param name="selected">Is this slot selected?</param>
    public void Select(bool selected)
    {
        if (selected)
        {
            OnSlotSelectChanged?.Invoke(true, this);
            EnableHighlight();
        }
        else
        {
            OnSlotSelectChanged?.Invoke(false, this);
            DisableHighlight();
        }
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

    /// <summary>
    /// Update data for this slot
    /// </summary>
    /// <param name="data">The new data</param>
    public void SetData(InventoryItem data)
    {
        _itemData = data;
        OnDataChange();
    }

    public InventoryItem GetData()
    {
        return _itemData;
    }

    public void Destroy()
    {
        OnSlotSelectChanged = null;
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<InventoryItem, InventorySlot> { };
}
