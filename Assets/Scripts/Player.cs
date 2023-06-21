using UnityEngine;

public class Player : Character
{
    [SerializeField] private InventorySystem _hotbar;
    [SerializeField] private InventorySystem _inventory;

    public InventorySystem Hotbar => _hotbar;
    public InventorySystem Inventory => _inventory;

    private void Awake()
    {
        if(_hotbar.Size == 0)
        {
            _hotbar = new InventorySystem(6);
        }
        if (_inventory.Size == 0)
        {
            _inventory = new InventorySystem(24);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GroundItem groundItem))
        {
            if (!_hotbar.TryForceAddItem(groundItem.Item, out _, out Item remainingItem))
            {
                if (!_inventory.TryForceAddItem(remainingItem, out _, out Item remainingItem2))
                {
                    remainingItem2.ItemData.Spawn(transform.position, remainingItem.Amount);
                    groundItem.PickUp();
                }
                else
                {
                    groundItem.PickUp();
                }
            }
            else
            {
                groundItem.PickUp();
            }
        }
    }
}
