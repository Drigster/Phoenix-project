using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InventorySlot
{
    [SerializeField] private Item _item;

    public UnityAction OnSlotUpdated;

    public Item Item => _item;
    public string Id;

    public bool IsFull => _item.Amount >= _item.ItemData.MaxStack;

    public InventorySlot(string id)
    {
        Id = id;
        _item = new Item(null, -1);
    }

    public void SetItem(Item item)
    {
        _item = item;
        OnSlotUpdated?.Invoke();
    }

    public void AddItem(Item item)
    {
        //if ((_item.Amount + item.Amount) > _item.ItemData.MaxStack) { throw new System.Exception("Item Amount cant be more than MaxStack"); }

        if (_item.ItemData == null || _item.Amount < 1)
        {
            _item = item;
        }
        else
        {
            _item.Add(item.Amount);
        }
        OnSlotUpdated?.Invoke();
    }

    public bool TryAddItem(Item item)
    {
        if (item.ItemData.ItemType != _item.ItemData.ItemType) { throw new System.Exception("Item type mismatch"); }

        if ((_item.Amount + item.Amount) > _item.ItemData.MaxStack) 
        {
            return false;
        }

        AddItem(item);
        OnSlotUpdated?.Invoke();

        return true;
    }

    public void SwapItems(InventorySlot slot)
    {
        Item tempItem = slot.Item;
        slot.SetItem(_item);
        SetItem(tempItem);
    }
}
