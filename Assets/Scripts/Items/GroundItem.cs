using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private TextMeshPro _amountText;
    [SerializeField] private Item _item;

    public Item Item => _item;

    public void PickUp()
    {
        Destroy(gameObject);
    }

    public void SetItem(ItemData itemData, int amount)
    {
        SetItem(new Item(itemData, amount));
    }

    public void SetItem(Item item)
    {
        _item = item;
        gameObject.name = _item.ItemData.Name;
        _renderer.sprite = _item.ItemData.UIIcon;
        if (_item.Amount > 1)
        {
            _amountText.text = _item.Amount.ToString();
        }
        else
        {
            _amountText.text = "";
        }
    }

    private bool TryStack(GroundItem sceneItem)
    {
        if (_item.ItemData == sceneItem.Item.ItemData && GetInstanceID() < sceneItem.GetInstanceID())
        {
            int tempAmount = (_item.Amount + sceneItem.Item.Amount) - _item.ItemData.MaxStack;
            if (tempAmount > 0)
            {
                _item.Add(_item.Amount - _item.ItemData.MaxStack);
                _item.ItemData.Spawn(transform.position, tempAmount);
            }
            else
            {
                _item.Add(sceneItem.Item.Amount);
            }
            _amountText.text = _item.Amount.ToString();
            return true;
        }
        return false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GroundItem sceneItem) && _item.Amount < _item.ItemData.MaxStack)
        {
            if (sceneItem.TryStack(this))
            {
                Destroy(gameObject);
            }
        }
    }
}
