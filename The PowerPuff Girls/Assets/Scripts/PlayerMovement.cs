
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum CharacterType
    {
        Bob,
        Patrick
    }

    public CharacterType characterType;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = 0f;

        if (Input.GetKey(leftKey))
            horizontal = -1f;

        if (Input.GetKey(rightKey))
            horizontal = 1f;

        rb.velocity = new Vector2(
            horizontal * moveSpeed,
            rb.velocity.y
        );

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
        }

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
