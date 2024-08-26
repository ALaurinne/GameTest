using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items = new List<Item>();
    public int size = 6;

    public void AddItem(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            Debug.Log(item.itemName + " added to inventory.");
        }
        else
        {
            Debug.Log(item.itemName + " is already in the inventory.");
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item.itemName + " removed from inventory.");
        }
        else
        {
            Debug.Log(item.itemName + " not found in inventory.");
        }
    }

    public void SwapItems(Item item1, Item item2)
    {
        if (items.Contains(item1) && items.Contains(item2))
        {
            int index1 = items.IndexOf(item1);
            int index2 = items.IndexOf(item2);

            items[index1] = item2;
            items[index2] = item1;
        }
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }
}