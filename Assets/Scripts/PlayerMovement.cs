using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove;
    public float speed;
    public float boost;
    public float distance;
    public Vector2 size;
    public LayerMask groundLayer;

    [Header("Jump System")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;

    Animator animator;
    Rigidbody2D rb;
    bool jump;
    float jumpCounter;

    Vector2 horizontalMove, gravity;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InitGame();
        gravity = new Vector2(0, -Physics2D.gravity.y);
    }

    public void InitGame()
    {
        GameManager.Instance.SetPlayer(this);
        canMove = true;
        animator.SetTrigger("Init");
        animator.SetBool("Win", false);
        animator.SetBool("Die", false);
    }

    public void Win()
    {
        canMove = false;
        animator.SetBool("Win", true);
    }

    public void Die()
    {
        canMove = false;
        animator.SetBool("Die", true);
    }

    public void Movement(InputAction.CallbackContext value)
    {
        horizontalMove = value.ReadValue<Vector2>();
        animator.SetFloat("Run", Mathf.Abs(horizontalMove.x));
        Flip();
    }

    public void Jump(InputAction.CallbackContext value)
    {
        if (value.started && isGrounded())
        {
            animator.SetTrigger("Jump");
            jump = true;
        }
        if (value.canceled)
        {
            jump = false;
        }
    }

    public bool isGrounded()
    {
        return (Physics2D.BoxCast(transform.position, size, 0, -transform.up, distance, groundLayer));
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        rb.velocity = new Vector2(horizontalMove.x * speed, rb.velocity.y);

        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * boost);
            jump = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.down * gravity.y * Time.deltaTime * fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !jump)
        {
            rb.velocity += Vector2.down * gravity.y * Time.deltaTime * (2* fallMultiplier / 3);
        }
    }

    void Flip()
    {
        var localScale = transform.localScale;

        if (horizontalMove.x < 0 && localScale.x > 0)
        {
            localScale.x *= -1;
        }
        else if (horizontalMove.x > 0 && localScale.x < 0)
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }

}
