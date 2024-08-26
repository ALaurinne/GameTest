using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    Image image;

    [SerializeField]
    TextMeshProUGUI amountText;

    [SerializeField]
    ItemDetails itemDetails;

    public event Action<SlotItem> OnLeftClickEvent;

    public event Action<SlotItem> OnPointerEnterEvent;

    public event Action<SlotItem> OnPointerExitEvent;

    public event Action<SlotItem> OnBeginDragEvent;

    public event Action<SlotItem> OnEndDragEvent;

    public event Action<SlotItem> OnDragEvent;

    public event Action<SlotItem> OnDropEvent;

    protected Color normalColor = Color.white;
    protected Color disabledColor = new Color(1, 1, 1, 0);
    protected Color dragColor = new Color(1, 1, 1, 0.5f);


    protected bool isPointerOver;
    private bool isDragging;

    protected Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null && Amount != 0) Amount = 0;

            if (_item == null)
            {
                image.sprite = null;
                image.color = disabledColor;
            }
            else
            {
                image.sprite = _item.icon;
                image.color = normalColor;
            }

            if (isPointerOver)
            {
                OnPointerExit(null);
                OnPointerEnter(null);
            }

        }
    }

    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            if (_amount < 0) _amount = 0;
            if (_amount == 0 && item != null) item = null;

            if (amountText != null)
            {
                amountText.enabled = _item != null && _amount > 1;
                if (amountText.enabled)
                {
                    amountText.text = _amount.ToString();
                }
            }
        }
    }

    public void OnDisable()
    {
        if (isDragging)
        {
            OnEndDrag(null);
        }

        if (isPointerOver)
        {
            OnPointerExit(null);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (OnDragEvent != null)
            OnDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        if (item != null)
            image.color = dragColor;

        if (OnBeginDragEvent != null)
            OnBeginDragEvent(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (item != null)
            image.color = normalColor;

        if (OnEndDragEvent != null)
            OnEndDragEvent(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClickEvent?.Invoke(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true; 

        if (OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;

        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);

    }
}
