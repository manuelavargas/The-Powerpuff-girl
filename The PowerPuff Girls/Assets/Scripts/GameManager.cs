using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI counterText;
    public TextMeshProUGUI timerText;

    [Header("Porta")]
    public ExitDoor exitDoor;

    [Header("Objetos da fase")]
    public int totalSpatulas = 3;
    public int totalBurgers = 3;

    [Header("Tempo")]
    public float timeRemaining = 60f;

    private int spatulasCollected = 0;
    private int burgersCollected = 0;

    private bool gameFinished = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateCounter();
        UpdateTimer();
    }

    private void Update()
    {
        if (gameFinished)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            gameFinished = true;
            TimeUp();
        }

        UpdateTimer();
    }

    public void CollectSpatula()
    {
        spatulasCollected++;
        UpdateCounter();

        CheckAllCollected();
    }

    public void CollectBurger()
    {
        burgersCollected++;
        UpdateCounter();

        CheckAllCollected();
    }

    private void UpdateCounter()
    {
       counterText.text = "E: " + spatulasCollected + "/" + totalSpatulas +
                   "        H: " + burgersCollected + "/" + totalBurgers;
    }

    private void UpdateTimer()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);

        timerText.text = "T: " + seconds;
    }

    private void CheckAllCollected()
    {
        if (spatulasCollected >= totalSpatulas &&
            burgersCollected >= totalBurgers)
        {
            Debug.Log("TODOS OS OBJETOS FORAM COLETADOS!");
        }
    }

    private void TimeUp()
    {
        Debug.Log("TEMPO ESGOTADO!");
    }
   public bool IsGameFinished()
{
    return gameFinished;
}
public bool AllObjectsCollected()
{
    return spatulasCollected >= totalSpatulas &&
           burgersCollected >= totalBurgers;
}
}