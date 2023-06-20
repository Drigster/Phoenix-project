using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private int _amount;

    public Item(ItemData itemData, int amount)
    {
        _itemData = itemData;
        _amount = amount;
    }

    public ItemData ItemData => _itemData;
    public int Amount => _amount;

    public void Add(int amount)
    {
        //if ((_amount + amount) > _itemData.MaxStack) { throw new System.Exception("Item Amount cant be more than MaxStack"); }
        if(_itemData == null)
        {
            throw new System.Exception("ItemData is null");
        }

        _amount = _amount + amount;
    }

    public void Remove(int amount)
    {
        if ((_amount - amount) < 0) { throw new System.Exception("Item Amount cant be less than 0"); }

        _amount = _amount - amount;
    }
}
