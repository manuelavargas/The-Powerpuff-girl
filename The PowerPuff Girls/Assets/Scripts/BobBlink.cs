using UnityEngine;
using System.Collections;

public class BobBlink : MonoBehaviour
{
    public Animator animator;
    public float tempoMinimo = 3f;
    public float tempoMaximo = 6f;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        StartCoroutine(PiscarAutomaticamente());
    }

    private IEnumerator PiscarAutomaticamente()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(tempoMinimo, tempoMaximo)
            );

            if (animator != null &&
                animator.GetCurrentAnimatorStateInfo(0)
                .IsName("bobanimation_idle"))
            {
                animator.SetTrigger("Blink");
            }
        }
    }
}