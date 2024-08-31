using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    AudioManager audioManager;

    AudioSource audioSource;
    Animator animator;
    Rigidbody2D rb;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("End"))
        {
            animator.SetBool("Win", true);
            audioSource?.PlayOneShot(audioManager.Win);
            GameManager.Instance.WinGame();
        }

        if (other.gameObject.CompareTag("Collectable") && InventoryManager.Instance.CanCollect)
        {
            audioSource?.PlayOneShot(audioManager.Collect);
            InventoryManager.Instance.CollectItem(other.GetComponent<CollectableItem>().item);
            Destroy(other.gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource?.PlayOneShot(audioManager.Hurt);
            animator.SetTrigger("Hurt");
            GameManager.Instance.Hurt();
            rb.AddForce(new Vector2(-1, rb.velocity.y));
        }
    }



}
