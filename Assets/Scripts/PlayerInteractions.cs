using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    private void Start()
    {
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
            GameManager.Instance.FinishGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Hurt");
            GameManager.Instance.Hurt();
            rb.AddForce(new Vector2(-1, rb.velocity.y));
        }
    }



}
