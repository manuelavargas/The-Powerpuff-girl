using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private bool doorUnlocked = false;

    private bool bobInside = false;
    private bool patrickInside = false;

    public void UnlockDoor()
    {
        doorUnlocked = true;

        Debug.Log("PORTA LIBERADA!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!doorUnlocked)
            return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        if (player.characterType == PlayerMovement.CharacterType.Bob)
        {
            bobInside = true;
            Debug.Log("BOB CHEGOU NA PORTA!");
        }

        if (player.characterType == PlayerMovement.CharacterType.Patrick)
        {
            patrickInside = true;
            Debug.Log("PATRICK CHEGOU NA PORTA!");
        }

        CheckPlayers();
    }

    private void CheckPlayers()
    {
        if (bobInside && patrickInside)
        {
            Debug.Log("FASE CONCLUÍDA!");
        }
    }
}