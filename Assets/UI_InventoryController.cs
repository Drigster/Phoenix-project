using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_InventoryController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UI_Inventory _hotbar;
    [SerializeField] private UI_Inventory _inventory;
    [SerializeField] private UI_Inventory _dynamicInventory;
    public static UnityAction<InventorySystem> OnInventoryChangeRequested;
    public static UnityAction<InventorySystem> OnInventoryCloseRequested;

    private void Start()
    {
        _hotbar.Set(_player.Hotbar);
        _inventory.Set(_player.Inventory);
        OnInventoryChangeRequested += UpdateDynamicInventory;
        OnInventoryCloseRequested += CloseDynamicInventory;
    }

    private void UpdateDynamicInventory(InventorySystem inventory)
    {
        _dynamicInventory.Set(inventory);
    }

    private void CloseDynamicInventory(InventorySystem inventory)
    {
        _dynamicInventory.Clear();
    }
}
