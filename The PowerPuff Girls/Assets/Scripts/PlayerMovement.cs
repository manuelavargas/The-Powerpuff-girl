
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public enum CharacterType { Bob, Patrick }

    public CharacterType characterType;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crawlSpeed = 2f;

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
    public KeyCode crouchKey;

    public float tempoMinimo = 3f;
    public float tempoMaximo = 6f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private float horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (characterType == CharacterType.Bob && animator != null)
        {
            animator.SetBool("IsCrouching", false);
            animator.SetBool("IsCrawling", false);
            StartCoroutine(PiscarAutomaticamente());
        }
    }

    void Update()
    {
        horizontal = 0f;

        if (Input.GetKey(leftKey))
            horizontal = -1f;

        if (Input.GetKey(rightKey))
            horizontal = 1f;

        bool agachando = characterType == CharacterType.Bob
                         && Input.GetKey(crouchKey)
                         && isGrounded;

        bool rastejando = agachando && Mathf.Abs(horizontal) > 0.1f;

        float velocidadeAtual = agachando ? crawlSpeed : moveSpeed;

        rb.velocity = new Vector2(
            horizontal * velocidadeAtual,
            rb.velocity.y
        );

        if (characterType == CharacterType.Bob)
        {
            if (horizontal < 0)
            {
                transform.localScale = new Vector3(
                    -Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
            else if (horizontal > 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
        }

        if (animator != null && characterType == CharacterType.Bob)
        {
            animator.SetBool("IsCrouching", agachando);
            animator.SetBool("IsCrawling", rastejando);
            animator.SetFloat("Speed", agachando ? 0f : Mathf.Abs(horizontal));
            animator.SetBool("IsGrounded", isGrounded);
        }

        if (!agachando && Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(
                rb.velocity.x,
                jumpForce
            );

            isGrounded = false;

            if (animator != null && characterType == CharacterType.Bob)
            {
                animator.SetBool("IsGrounded", false);
            }
        }
    }

    private IEnumerator PiscarAutomaticamente()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(tempoMinimo, tempoMaximo)
            );

            if (animator != null &&
                Mathf.Abs(horizontal) < 0.1f &&
                animator.GetCurrentAnimatorStateInfo(0)
                    .IsName("bobanimation_idle"))
            {
                animator.SetTrigger("Blink");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (animator != null &&
                characterType == CharacterType.Bob)
            {
                animator.SetBool("IsGrounded", true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

            if (animator != null &&
                characterType == CharacterType.Bob)
            {
                animator.SetBool("IsGrounded", false);
            }
        }
    }
}
