using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    [SerializeField] private InventorySystem _hotbar;
    [SerializeField] private InventorySystem _inventory;

    private PlayerInput _playerInput;

    public InventorySystem Hotbar => _hotbar;
    public InventorySystem Inventory => _inventory;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        if(_hotbar.Size == 0)
        {
            _hotbar = new InventorySystem(6);
        }
        if (_inventory.Size == 0)
        {
            _inventory = new InventorySystem(24);
        }
    }

    private void OnEnable()
    {
        _playerInput.UI.OpenUI.Enable();
    }

    private void OnDisable()
    {
        _playerInput.UI.OpenUI.Disable();
    }

    private void Update()
    {
        if (_playerInput.UI.OpenUI.WasPerformedThisFrame())
        {
            //UiController.OnPlayerInventoryOpenRequsted?.Invoke();
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
