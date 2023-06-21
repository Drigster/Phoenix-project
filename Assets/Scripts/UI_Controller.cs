using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private List<UI_Menu> _hud;
    [SerializeField] private List<UI_Menu> _menus;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        for (int i = 0; i < _hud.Count; i++)
        {
            _hud[i] = Instantiate(_hud[i], transform);
            _hud[i].Show();
        }

        for (int i = 0; i < _menus.Count; i++)
        {
            _menus[i] = Instantiate(_menus[i], transform);
            _menus[i].Hide();
        }
    }

    private void OnEnable()
    {
        _playerInput.UI.CloseUI.Enable();
        _playerInput.UI.CloseUI.performed += (InputAction.CallbackContext _) => ToggleMenu(-1);
        for (int i = 0; i < _menus.Count; i++)
        {
            UI_Menu menu = _menus[i];
            if (menu.OpenAction == null ) {
                continue;
            }
            menu.OpenAction.action.Enable();
            int j = i;
            menu.OpenAction.action.performed += (InputAction.CallbackContext _) => ToggleMenu(j);
        }
    }

    private void OnDisable()
    {
        _playerInput.UI.CloseUI.performed -= (InputAction.CallbackContext _) => ToggleMenu(-1);
        _playerInput.UI.CloseUI.Disable();
        for (int i = 0; i < _menus.Count; i++)
        {
            UI_Menu menu = _menus[i];
            if (menu.OpenAction == null)
            {
                continue;
            }

            int j = i;
            menu.OpenAction.action.performed -= (InputAction.CallbackContext _) => ToggleMenu(j);
            menu.OpenAction.action.Disable();
        }
    }

    public void ToggleMenu(int index)
    {
        for (int i = 0; i < _menus.Count; i++)
        {
            if (i == index && !_menus[i].IsActive)
            {
                _menus[i].Show();
            }
            else
            {
                _menus[i].Hide();
            }
        }
    }

    public void OpenMenu(string id, bool closeOldMenus = true)
    {
        for (int i = 0; i < _menus.Count; i++)
        {
            if (_menus[i].Id == id)
            {
                _menus[i].Show();
            }
            else if (closeOldMenus)
            {
                _menus[i].Hide();
            }
        }
    }

    public void CloseMenu(string id)
    {
        for (int i = 0; i < _menus.Count; i++)
        {
            if (_menus[i].Id == id)
            {
                _menus[i].Hide();
            }
        }
    }
}
