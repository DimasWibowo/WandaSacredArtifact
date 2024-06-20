using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void JumpAction();
    public static event JumpAction OnJump;

    float hAxis;
    Vector2 direction;

    [SerializeField]
    float speed = 8;

    [SerializeField]
    float jumpPower = 8;

    Rigidbody2D rb;
    Animator animator;

    [SerializeField]
    bool onGround = false;

    int jumpCount = 0;
    int maxJumpCount = 2;

    private Transform originalParent;

    [SerializeField]
    private CircleCollider2D groundCheckCollider; // Reference to the ground check CircleCollider2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalParent = transform.parent;
    }

    void Update()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.isDialogueActive)
        {
            HandleDialogueFreeze();
        }
        else
        {
            Movement();
            Jump();
            Facing();
            Animations();
        }
    }

    void HandleDialogueFreeze()
    {
        // Hanya berhenti pergerakan horizontal dan vertikal, tapi animasi tetap berjalan
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetFloat("Moving", 0);
        animator.SetBool("OnGround", onGround);
    }

    void Animations()
    {
        animator.SetFloat("Moving", Mathf.Abs(hAxis));
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("OnGround", onGround);
    }

    void Movement()
    {
        hAxis = Input.GetAxis("Horizontal");
        direction = new Vector2(hAxis, rb.velocity.y);

        rb.velocity = new Vector2(hAxis * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (onGround || jumpCount < maxJumpCount))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount++;
            onGround = false;
            animator.SetBool("OnGround", !onGround);

            OnJump?.Invoke(); // Trigger the OnJump event
        }
    }

    private void Facing()
    {
        if (hAxis < 0)
        {
            transform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
        }
        else if (hAxis > 0)
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ground"))
        {
            foreach (ContactPoint2D point in col.contacts)
            {
                if (point.normal.y > 0.5f) // Ensure collision is from below
                {
                    onGround = true;
                    animator.SetBool("OnGround", !onGround);
                    jumpCount = 0;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ground"))
        {
            onGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MovePlat") && col.gameObject.activeInHierarchy)
        {
            onGround = true;
            jumpCount = 0;
            transform.SetParent(col.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MovePlat") && col.gameObject.activeInHierarchy)
        {
            onGround = false;
            transform.SetParent(originalParent);
        }
    }
}
