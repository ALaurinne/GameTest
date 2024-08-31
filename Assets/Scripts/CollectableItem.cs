using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;
    }
}

