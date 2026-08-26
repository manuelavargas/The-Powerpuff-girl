using UnityEngine;

public class Collectible : MonoBehaviour
{
    public PlayerMovement.CharacterType characterWhoCanCollect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        // Se o tempo acabou, não permite coletar
        if (GameManager.Instance.IsGameFinished())
            return;

        if (player.characterType == characterWhoCanCollect)
        {
            if (characterWhoCanCollect == PlayerMovement.CharacterType.Bob)
            {
                GameManager.Instance.CollectSpatula();
            }
            else if (characterWhoCanCollect == PlayerMovement.CharacterType.Patrick)
            {
                GameManager.Instance.CollectBurger();
            }

            Destroy(gameObject);
        }
    }
}