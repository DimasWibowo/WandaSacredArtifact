using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Basic Movement
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDoubleJump;
    private bool isJumping;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        // Periksa apakah dialog aktif
        if (DialogueManager.Instance.isDialogueActive)
        {
            horizontal = 0;
            return;
        }

        // Handle horizontal movement input
        horizontal = Input.GetAxisRaw("Horizontal");

        // Handle jumping logic
        HandleJump();

        // Flip character sprite based on movement direction
        Flip();

        // Coyote time and jump buffering logic
        CoyoteAndBuffer();
    }

    private void FixedUpdate()
    {
        // Periksa apakah dialog aktif
        if (DialogueManager.Instance.isDialogueActive)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        // Apply horizontal movement
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void HandleJump()
    {
        if (DialogueManager.Instance.isDialogueActive)
        {
            return;
        }

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpCooldown());
        }
        else if (canDoubleJump && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            canDoubleJump = false;
            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void CoyoteAndBuffer()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.2f); // Waktu cooldown bisa disesuaikan
        isJumping = false;
    }
}
