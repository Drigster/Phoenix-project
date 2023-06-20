using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _openSprite;
    [SerializeField] private Sprite _closedSprite;
    [SerializeField] private int _inventorySize;
    [SerializeField] private InventorySystem _inventorySystem;
    private SpriteRenderer _spriteRenderer;

    public UnityAction<IInteractable> OnIteractionComplete { get; set; }

    private void Awake()
    {
        //UiController.OnInventoryCloseRequsted += EndInteraction;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inventorySystem = new InventorySystem(_inventorySize);
    }

    public void Interact(Interactor interactor, out bool interactionSuccesful)
    {
        UI_InventoryController.OnInventoryChangeRequested?.Invoke(_inventorySystem);
        _spriteRenderer.sprite = _openSprite;
        interactionSuccesful = true;
    }

    public void EndInteraction()
    {
        UI_InventoryController.OnInventoryCloseRequested?.Invoke(_inventorySystem);
        _spriteRenderer.sprite = _closedSprite;
    }

    public void EndInteraction(InventorySystem _)
    {
        _spriteRenderer.sprite = _closedSprite;
    }
}
