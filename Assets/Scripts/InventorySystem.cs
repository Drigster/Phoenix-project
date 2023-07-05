using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> _slots;
    [SerializeField] private int _selectedIndex = -1;

    public int Size => _slots.Count;
    public List<InventorySlot> Slots => _slots;
    public InventorySlot SelectedSlot => Slots[_selectedIndex];
    public int SelectedIndex => _selectedIndex;

    public InventorySystem(int size)
    {
        _slots = new List<InventorySlot>();
        for (int i = 0; i < size; i++)
        {
            _slots.Add(new InventorySlot("" + i));
        }
    }

    public bool TryForceAddItem(Item item, out List<InventorySlot> inventorySlots, out Item remainingItem)
    {
        inventorySlots = new List<InventorySlot>();
        remainingItem = null;
        if (TryGetSlotsByType(item, out List<InventorySlot> slots))
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.TryAddItem(slot.Item))
                {
                    //OnInventorySlotChanged?.Invoke(slot);
                    inventorySlots.Add(slot);
                    return true;
                }
                else if (!slot.IsFull)
                {
                    Debug.Log("!slot.IsFull");
                    Item tempItem = new Item(item.ItemData, slot.Item.ItemData.MaxStack - slot.Item.Amount);
                    slot.AddItem(tempItem);
                    inventorySlots.Add(slot);
                    if ((item.Amount - tempItem.Amount) < 1)
                    {
                        return true;
                    }
                    else
                    {
                        item.Remove(tempItem.Amount);
                    }
                    //OnInventorySlotChanged?.Invoke(slot);
                }
            }
        }

        if (TryGetEmptySlots(out List<InventorySlot> emptySlots))
        {
            emptySlots[0].AddItem(item);
            //OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        remainingItem = item;
        return false;
    }

    public bool TryGetSlotsByType(Item item, out List<InventorySlot> slots)
    {
        return TryGetSlotsByType(item.ItemData.ItemType, out slots);
    }

    public bool TryGetSlotsByType(ItemData itemData, out List<InventorySlot> slots)
    {
        return TryGetSlotsByType(itemData.ItemType, out slots);
    }

    public bool TryGetSlotsByType(ItemData.ItemTypes itemType, out List<InventorySlot> slots)
    {
        slots = _slots.Where(i => i.Item != null).Where(i => i.Item.ItemData != null).Where(i => i.Item.ItemData.ItemType == itemType).ToList();

        return slots.Count > 0;
    }

    public bool TryGetEmptySlots(out List<InventorySlot> slots)
    {
        slots = _slots.Where(i => i.Item != null).Where(i => i.Item.ItemData == null).ToList();

        return slots.Count > 0;
    }

    public void SelectSlot(int index)
    {
        _selectedIndex = index;
    }
}
