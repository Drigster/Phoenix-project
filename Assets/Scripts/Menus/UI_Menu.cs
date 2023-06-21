using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] protected Transform _container;
    [SerializeField] private InputActionReference _openAction;
    [SerializeField] private string _id;
    public InputActionReference OpenAction => _openAction;
    public string Id => _id;
    public bool IsActive => _container.gameObject.activeInHierarchy;

    public void Show()
    {
        _container.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _container.gameObject.SetActive(false);
    }
}
