using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class UI_Hotbar : UI_Inventory
{
    [SerializeField] private int _selectedIndex = -1;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    void Start()
    {
        Player player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            Set(player.Hotbar);
        }
    }

    private void Update()
    {
        List<UI_InventorySlot> slotElements = _container.GetComponentsInChildren<UI_InventorySlot>().ToList();
        if (_playerInput.UI.Hotbar.WasPerformedThisFrame())
        {
            if (int.TryParse(_playerInput.UI.Hotbar.activeControl.name, out int keyNumber))
            {
                bool wasSelected = false;
                for (int i = 0; i < slotElements.Count; i++)
                {
                    if (i == keyNumber - 1)
                    {
                        slotElements[i].SetSelected(true);
                        _selectedIndex = i;
                        wasSelected = true;
                    }
                    else
                    {
                        slotElements[i].SetSelected(false);
                    }
                }
                if(!wasSelected)
                {
                    _selectedIndex = -1;
                }
            }
        }
        if (_playerInput.UI.HotbarScroll.WasPerformedThisFrame())
        {
            if(_playerInput.UI.HotbarScroll.ReadValue<float>() > 0)
            {
                if(_selectedIndex == slotElements.Count - 1)
                {
                    _selectedIndex = 0;
                }
                else
                {
                    _selectedIndex++;
                }
            }
            else
            {
                if (_selectedIndex == 0)
                {
                    _selectedIndex = slotElements.Count - 1;
                }
                else
                {
                    _selectedIndex--;
                }
            }
            for (int i = 0; i < slotElements.Count; i++)
            {
                if (i == _selectedIndex)
                {
                    slotElements[i].SetSelected(true);
                }
                else
                {
                    slotElements[i].SetSelected(false);
                }
            }
        }
    }

    private void OnEnable()
    {
        _playerInput.UI.Hotbar.Enable();
        _playerInput.UI.HotbarScroll.Enable();
    }

    private void OnDisable()
    {
        _playerInput.UI.Hotbar.Disable();
        _playerInput.UI.HotbarScroll.Disable();
    }
}
