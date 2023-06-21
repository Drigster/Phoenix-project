using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Inventory : UI_Menu
{
    [SerializeField] protected InventorySystem _inventorySystem;
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
