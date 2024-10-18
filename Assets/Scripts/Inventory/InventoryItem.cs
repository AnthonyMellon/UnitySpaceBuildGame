using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "SpaceGame/Inventory/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _Icon;

    public string GetName()
    {
        return _name;
    }

    public Sprite GetIcon()
    {
        return _Icon;
    }
}
