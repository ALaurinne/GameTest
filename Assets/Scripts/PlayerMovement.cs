using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove;
    public float speed;
    public float jumpForce;
    public float boost;
    public float distance;
    public Vector2 size;
    public LayerMask groundLayer;

    Animator animator;
    Rigidbody2D rb;

    float horizontalMove;
    bool jump;
    bool facingRight;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InitGame();
    }

    public void InitGame()
    {
        canMove = true;
        facingRight = true;
        animator.SetTrigger("Init");
        animator.SetBool("Win", false);
        animator.SetBool("Die", false);
    }

    public void Die()
    {
        animator.SetBool("Die", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        horizontalMove = Input.GetAxis("Horizontal") * speed;

        animator.SetFloat("Run", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (jump && isGrounded())
        {
            animator.SetBool("Jump", true);
        }
        if (!jump && isGrounded())
        {
            animator.SetBool("Jump", false);
        }
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, size, 0, -transform.up, distance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void FixedUpdate()
    {
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
        }
        if (jump && !isGrounded())
        {
            jump = false;
        }
        if (isGrounded() && jump)
        {
            rb.AddForce(new Vector2(rb.velocity.x * 0.5f, jumpForce * 10 * boost));
            jump = false;

        }

        if (horizontalMove > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalMove < 0 && facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        var localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
