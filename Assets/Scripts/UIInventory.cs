using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryParent;

    [SerializeField]
    SlotItem[] itemsSlot;

    [SerializeField]
    Inventory inventory;

    [SerializeField]
    Button CloseInventoryButton;

    public DropItemArea dropArea;

    public Image draggableItem;

    public ItemDetails itemDetails;


    public event Action<SlotItem> OnLeftClickEvent;

    public event Action<SlotItem> OnPointerEnterEvent;

    public event Action<SlotItem> OnPointerExitEvent;

    public event Action<SlotItem> OnBeginDragEvent;

    public event Action<SlotItem> OnEndDragEvent;

    public event Action<SlotItem> OnDragEvent;

    public event Action<SlotItem> OnDropEvent;


    private void OnValidate()
    {
        if (inventoryParent != null)
            itemsSlot = inventoryParent.GetComponentsInChildren<SlotItem>();

        RefreshUI();
    }

    private void Start()
    {
        foreach (SlotItem item in itemsSlot)
        {
            item.OnLeftClickEvent += OnLeftClickEvent;
            item.OnPointerEnterEvent += OnPointerEnterEvent;
            item.OnPointerExitEvent += OnPointerExitEvent;

            item.OnBeginDragEvent += OnBeginDragEvent;
            item.OnEndDragEvent += OnEndDragEvent;

            item.OnDragEvent += OnDragEvent;
            item.OnDropEvent += OnDropEvent;
        }

        CloseInventoryButton.onClick.AddListener(CloseInventory);
    }

    private void CloseInventory()
    {
        GameManager.Instance.ToggleInventory(false);
    }

    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {

        for (int i = 0; i < itemsSlot.Length; i++)
        {
            if (inventory.items.Count > i)
            {
                itemsSlot[i].item = inventory.items[i];
            }
            else
            {
                itemsSlot[i].item = null;
            }
        }

    }
}
