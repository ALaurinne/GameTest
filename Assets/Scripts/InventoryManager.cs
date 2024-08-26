using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private UIInventory UIinventory;

    [SerializeField]
    private DropItemArea dropArea;

    [SerializeField]
    Image draggableItem;

    [SerializeField]
    ItemDetails itemDetails;

    SlotItem draggedSlot;

    public bool CanCollect => inventory.items.Count < 6;

    private bool isPinned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        UIinventory.OnDragEvent += OnDrag;
        UIinventory.OnDropEvent += OnDrop;
        UIinventory.OnEndDragEvent += OnEndDrag;
        UIinventory.OnBeginDragEvent += OnBeginDrag;
        UIinventory.OnPointerEnterEvent += ShowInfo;
        UIinventory.OnPointerExitEvent += HideInfo;
        UIinventory.OnLeftClickEvent += PinInfo;
        dropArea.OnDropEvent += DropOutside; 
    }

    public void PinInfo(SlotItem slotItem)
    {
        isPinned = true;
        StartCoroutine(RemovePin());
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
        draggableItem.color = new Color(1, 1, 1, 0);
        draggableItem.enabled = false;
        draggedSlot.item = null;
    }

    public void ShowInfo(SlotItem slotItem)
    {
        if(isPinned) return;

        if (slotItem.item != null)
        {
            itemDetails.Item = slotItem.item;
            itemDetails.gameObject.SetActive(true);
        }
    }

    public void HideInfo(SlotItem item)
    {
        if (isPinned) return;

        if (itemDetails.gameObject.activeSelf)
        {
            itemDetails.Item = null;
            itemDetails.gameObject.SetActive(false);
        }
    }
    
    public void CollectItem(Item item)
    {
        if(inventory.items.Count < 6) {
        if (!inventory.ContainsItem(item))
        {
            inventory.AddItem(item);
        }
        else
        {
            Debug.Log("Already in inventory");
        }
        } else
        {
            Debug.Log("Inventory is full");
        }

        UIinventory.RefreshUI();
    }

    public void OnDrag(SlotItem slotItem)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    public void OnDrop(SlotItem slotItem)
    {
        if (draggableItem.enabled)
        {
            draggableItem.color = new Color(1, 1, 1, 0);
            draggableItem.enabled = false;
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
            draggableItem.sprite = slotItem.item.icon;
            draggableItem.color = new Color (1, 1, 1, 1);
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    public void OnEndDrag(SlotItem slotItem)
    {
        if (slotItem.item != null)
        {
            draggedSlot = null;
            draggableItem.color = new Color(1, 1, 1, 0);
            draggableItem.enabled = false;
        }
    }

    public void UseItem(Item item)
    {
        if (itemDetails.gameObject.activeSelf)
        {
            itemDetails.Item = null;
            itemDetails.gameObject.SetActive(false);
        }

        inventory.RemoveItem(item);
        UIinventory.RefreshUI();
    }


    public void DropItem(Item item)
    { 
        UIinventory.RefreshUI();
    }

    public void ClearInventory()
    {
        inventory.items.Clear();
    }
}