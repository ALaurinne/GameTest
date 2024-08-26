using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    AudioSource audioSource;

    public Item item;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && InventoryManager.Instance.CanCollect)
        {
            audioSource?.Play();
            InventoryManager.Instance.CollectItem(item);
            Destroy(gameObject);

        }
    }
}

