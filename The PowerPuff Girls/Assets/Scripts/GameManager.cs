using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // =====================================================
    // SINGLETON
    // =====================================================

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    // =====================================================
    // UI
    // =====================================================

    public TextMeshProUGUI counterText;
    public TextMeshProUGUI timerText;


    // =====================================================
    // OBJETOS DA FASE
    // =====================================================

    public int totalSpatulas = 3;
    public int totalBurgers = 3;

    private int spatulasCollected = 0;
    private int burgersCollected = 0;


    // =====================================================
    // TEMPO
    // =====================================================

    public float timeRemaining = 60f;

    private bool gameFinished = false;


    // =====================================================
    // PORTAS
    // =====================================================

    private bool bobReachedDoor = false;
    private bool patrickReachedDoor = false;


    // =====================================================
    // INÍCIO
    // =====================================================

    private void Start()
    {
        UpdateCounter();
        UpdateTimer();
    }


    // =====================================================
    // TIMER
    // =====================================================

    private void Update()
    {
        if (gameFinished)
            return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            UpdateTimer();
        }
        else
        {
            timeRemaining = 0;
            UpdateTimer();

            TimeUp();
        }
    }


    private void UpdateTimer()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);

        timerText.text = "TEMPO: " + seconds;
    }


    private void TimeUp()
    {
        gameFinished = true;

        Debug.Log("TEMPO ESGOTADO!");
    }


    // =====================================================
    // COLETAR ESPÁTULA
    // =====================================================

    public void CollectSpatula()
    {
        if (gameFinished)
            return;

        spatulasCollected++;

        UpdateCounter();

        CheckAllCollected();
    }


    // =====================================================
    // COLETAR HAMBÚRGUER
    // =====================================================

    public void CollectBurger()
    {
        if (gameFinished)
            return;

        burgersCollected++;

        UpdateCounter();

        CheckAllCollected();
    }


    // =====================================================
    // ATUALIZAR CONTADOR
    // =====================================================

    private void UpdateCounter()
    {
        counterText.text =
            "ESPATULAS: " + spatulasCollected + "/" + totalSpatulas +
            "        HAMBURGUERES: " + burgersCollected + "/" + totalBurgers;
    }


    // =====================================================
    // VERIFICAR SE PEGOU TUDO
    // =====================================================

    private void CheckAllCollected()
    {
        if (spatulasCollected >= totalSpatulas &&
            burgersCollected >= totalBurgers)
        {
            Debug.Log("TODOS OS OBJETOS FORAM COLETADOS!");
        }
    }


    // =====================================================
    // VERIFICAR SE TODOS OS OBJETOS FORAM COLETADOS
    // =====================================================

    public bool AllObjectsCollected()
    {
        return spatulasCollected >= totalSpatulas &&
               burgersCollected >= totalBurgers;
    }


    // =====================================================
    // VERIFICAR SE O JOGO TERMINOU
    // =====================================================

    public bool IsGameFinished()
    {
        return gameFinished;
    }


    // =====================================================
    // BOB CHEGOU NA PORTA
    // =====================================================

    public void BobReachedDoor()
    {
        bobReachedDoor = true;

        Debug.Log("BOB CHEGOU NA PORTA DELE!");

        CheckLevelComplete();
    }


    // =====================================================
    // PATRICK CHEGOU NA PORTA
    // =====================================================

    public void PatrickReachedDoor()
    {
        patrickReachedDoor = true;

        Debug.Log("PATRICK CHEGOU NA PORTA DELE!");

        CheckLevelComplete();
    }


    // =====================================================
    // VERIFICAR FINAL DA FASE
    // =====================================================

    private void CheckLevelComplete()
    {
        // Ainda falta algum objeto
        if (!AllObjectsCollected())
            return;

        // Ainda falta algum personagem chegar
        if (!bobReachedDoor || !patrickReachedDoor)
            return;

        // Os dois chegaram!
        Debug.Log("================================");
        Debug.Log("🎉 FASE 1 CONCLUÍDA!");
        Debug.Log("================================");
    }
}