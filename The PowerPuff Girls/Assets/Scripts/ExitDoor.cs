using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public PlayerMovement.CharacterType characterAllowed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        // A porta só funciona depois que TODOS os objetos foram coletados
        if (!GameManager.Instance.AllObjectsCollected())
            return;

        // Verifica se é o personagem correto
        if (player.characterType != characterAllowed)
            return;

        if (characterAllowed == PlayerMovement.CharacterType.Bob)
        {
            Debug.Log("BOB CHEGOU NA PORTA DELE!");

            GameManager.Instance.BobReachedDoor();
        }

        if (characterAllowed == PlayerMovement.CharacterType.Patrick)
        {
            Debug.Log("PATRICK CHEGOU NA PORTA DELE!");

            GameManager.Instance.PatrickReachedDoor();
        }
    }
}