using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_DynamicInventory : UI_Inventory
{
    public static UnityAction<InventorySystem> OnInventoryChangeRequested;
    public static UnityAction OnInventoryCloseRequested;
    private UI_Controller _controller;

    private void Awake()
    {
        OnInventoryChangeRequested += Open;
        OnInventoryCloseRequested += Close;
        _controller = FindAnyObjectByType<UI_Controller>();
    }

    private void Open(InventorySystem inventorySystem)
    {
        if(_controller != null)
        {
            Set(inventorySystem);
            _controller.OpenMenu(Id);
            _controller.OpenMenu("playerInventory", false);
        }
    }

    private void Close()
    {
        _controller.CloseMenu(Id);
        _controller.CloseMenu("playerInventory");
    }
}
