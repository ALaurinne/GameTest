using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private UIInventory inventoryPanel;

    SlotItem draggedSlot;

    public bool CanCollect => inventory.items.Count < 6;

    private bool isPinned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        inventoryPanel.OnDragEvent += OnDrag;
        inventoryPanel.OnDropEvent += OnDrop;
        inventoryPanel.OnEndDragEvent += OnEndDrag;
        inventoryPanel.OnBeginDragEvent += OnBeginDrag;
        inventoryPanel.OnPointerEnterEvent += ShowInfo;
        inventoryPanel.OnPointerExitEvent += HideInfo;
        inventoryPanel.OnLeftClickEvent += PinInfo;
        inventoryPanel.dropArea.OnDropEvent += DropOutside;
    }

    public void SetUIInventory(UIInventory inventoryPanel)
    {
        this.inventoryPanel = inventoryPanel;
    }

    public void PinInfo(SlotItem slotItem)
    {
        if (slotItem.item as UsableItem != null)
        {
            isPinned = true;
            StartCoroutine(RemovePin());
        }
    }

    IEnumerator RemovePin()
    {
        yield return new WaitForSeconds(10);
        isPinned = false;
        yield break;
    }

    public void DropOutside()
    {
        inventory.RemoveItem(draggedSlot.item);
        inventoryPanel.draggableItem.color = new Color(1, 1, 1, 0);
        inventoryPanel.draggableItem.enabled = false;
        draggedSlot.item = null;
    }

    public void ShowInfo(SlotItem slotItem)
    {
        if (isPinned) return;

        if (slotItem.item != null)
        {
            inventoryPanel.itemDetails.Item = slotItem.item;
            inventoryPanel.itemDetails.gameObject.SetActive(true);
        }
    }

    public void HideInfo(SlotItem item)
    {
        if (isPinned) return;

        if (inventoryPanel.itemDetails.gameObject.activeSelf)
        {
            inventoryPanel.itemDetails.Item = null;
            inventoryPanel.itemDetails.gameObject.SetActive(false);
        }
    }

    public void CollectItem(Item item)
    {
        if (inventory.items.Count < 6)
        {
            if (!inventory.ContainsItem(item))
            {
                inventory.AddItem(item);
            }
        }
        else
        {
            Debug.Log("Inventory is full");
        }

        inventoryPanel.RefreshUI();
    }

    public void OnDrag(SlotItem slotItem)
    {
        if (inventoryPanel.draggableItem.enabled)
        {
            inventoryPanel.draggableItem.transform.position = Input.mousePosition;
        }
    }

    public void OnDrop(SlotItem slotItem)
    {
        if (inventoryPanel.draggableItem.enabled)
        {
            inventoryPanel.draggableItem.color = new Color(1, 1, 1, 0);
            inventoryPanel.draggableItem.enabled = false;
        }

        Item draggedItem = draggedSlot.item;
        inventory.SwapItems(draggedSlot.item, slotItem.item);
        draggedSlot.item = slotItem.item;
        slotItem.item = draggedItem;
    }

    public void OnBeginDrag(SlotItem slotItem)
    {
        if (slotItem.item != null)
        {
            draggedSlot = slotItem;
            inventoryPanel.draggableItem.sprite = slotItem.item.icon;
            inventoryPanel.draggableItem.color = new Color(1, 1, 1, 1);
            inventoryPanel.draggableItem.transform.position = Input.mousePosition;
            inventoryPanel.draggableItem.enabled = true;
        }
    }

    public void OnEndDrag(SlotItem slotItem)
    {
        if (slotItem.item != null)
        {
            draggedSlot = null;
            inventoryPanel.draggableItem.color = new Color(1, 1, 1, 0);
            inventoryPanel.draggableItem.enabled = false;
        }
    }

    public void UseItem(Item item)
    {
        if (inventoryPanel.itemDetails.gameObject.activeSelf)
        {
            inventoryPanel.itemDetails.Item = null;
            inventoryPanel.itemDetails.gameObject.SetActive(false);
        }

        isPinned = false;
        inventory.RemoveItem(item);
        inventoryPanel.RefreshUI();
    }


    public void DropItem(Item item)
    {
        inventoryPanel.RefreshUI();
    }

    public void ClearInventory()
    {
        inventory.items.Clear();
    }
}