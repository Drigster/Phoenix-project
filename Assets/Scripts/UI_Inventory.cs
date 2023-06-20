using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private UI_InventorySlot _inventorySlotPrefab;

    private void OnEnable()
    {
        Reload();
    }

    public void Set(InventorySystem inventorySystem)
    {
        _inventorySystem = inventorySystem;
        Reload();
    }

    public void Reload()
    {
        foreach (Transform children in _container)
        {
            Destroy(children.gameObject);
        }
        if (_inventorySystem == null)
        {
            return;
        }

        foreach (InventorySlot inventorySlot in _inventorySystem.Slots)
        {
            UI_InventorySlot slot = Instantiate(_inventorySlotPrefab, _container);
            slot.Reload(inventorySlot);
        }
    }

    public void Clear()
    {
        _inventorySystem = null;
        Reload();
    }
}
