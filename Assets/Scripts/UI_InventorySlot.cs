using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class UI_InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private Image _selection;
    [SerializeField] private bool _isSelected = false;
    public bool IsSelected => _isSelected;
    private InventorySlot _assignedSlot;
    private Transform _parrent;
    private Vector2 _pos;

    public void Reload()
    {
        if(_assignedSlot == null || _assignedSlot.Item == null || _assignedSlot.Item.ItemData == null)
        {
            Clear();
            return;
        }

        _image.enabled = true;
        _image.sprite = _assignedSlot.Item.ItemData.UIIcon;
        _amountText.text = _assignedSlot.Item.Amount < 1 ? "" : "" + _assignedSlot.Item.Amount;
    }

    private void OnDestroy()
    {
        if(_assignedSlot == null) { return; }
        _assignedSlot.OnSlotUpdated -= Reload;
    }

    public void Reload(InventorySlot inventorySlot)
    {
        if(inventorySlot == null)
        {
            Clear();
            return;
        }
        if(_assignedSlot != inventorySlot && _assignedSlot != null)
        {
            _assignedSlot.OnSlotUpdated -= Reload;
        }
        _assignedSlot = inventorySlot;
        _assignedSlot.OnSlotUpdated += Reload;

        if (inventorySlot.Item == null || inventorySlot.Item.ItemData == null)
        {
            Clear();
            return;
        }
        Reload();
    }

    public void Clear()
    {
        _image.enabled = false;
        _image.sprite = null;
        _amountText.text = "";
    }

    public void OnDrag(PointerEventData eventData)
    {
        _image.transform.position = Input.mousePosition;
        _amountText.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.transform.SetParent(_parrent);
        _image.transform.position = _pos;
        _image.raycastTarget = true;
        _amountText.transform.SetParent(_parrent);
        _amountText.transform.position = _pos;
        _amountText.raycastTarget = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parrent = _image.transform.parent;
        _pos = _image.transform.position;
        _image.transform.SetParent(transform.root);
        _image.transform.SetAsLastSibling();
        _image.raycastTarget = false;
        _amountText.transform.SetParent(transform.root);
        _amountText.transform.SetAsLastSibling();
        _amountText.raycastTarget = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UI_InventorySlot slot))
        {
            slot._assignedSlot.SwapItems(_assignedSlot);
            Reload();
        }
    }

    public void SetSelected(bool selected)
    {
        _selection.gameObject.SetActive(selected);
    }
}
