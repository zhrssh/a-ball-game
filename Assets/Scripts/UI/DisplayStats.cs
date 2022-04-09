using UnityEngine;
using TMPro;
using DevZhrssh.Managers.Components;

public class DisplayStats : MonoBehaviour
{
    // Score
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI deathScore;
    [SerializeField] private TextMeshProUGUI highScore;

    [SerializeField] private TextMeshProUGUI timeoutScore;
    [SerializeField] private TextMeshProUGUI timeoutHighScore;
    private ScoreComponent scoreComponent;

    // Currency
    [SerializeField] private TextMeshProUGUI coinCount;
    private CoinCount coinCountScript;

    // Time
    [SerializeField] private TextMeshProUGUI time;
    private TimeComponent timeComponent;

    private void Start()
    {
        coinCountScript = GameObject.FindObjectOfType<CoinCount>();
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        timeComponent = GameObject.FindObjectOfType<TimeComponent>();
    }

    private void Update()
    {
        DisplayHUD(); // Gameplay HUD
        DeathHUD();
        TimeoutHUD();
    }

    private void DisplayHUD()
    {
        // Update score
        if (score != null)
            score.text = scoreComponent.playerScore.ToString();

        if (time != null)
            time.text = Mathf.FloorToInt(timeComponent.currentTime).ToString();

        if (coinCount != null)
            coinCount.text = coinCountScript.GetCoinCount().ToString();
    }

    private void DeathHUD()
    {
        if (deathScore != null)
            deathScore.text = scoreComponent.playerScore.ToString();

        if (highScore != null)
            highScore.text = scoreComponent.highScore.ToString();
    }

    private void TimeoutHUD()
    {
        if (timeoutScore != null)
            timeoutScore.text = scoreComponent.playerScore.ToString();

        if (timeoutHighScore != null)
            timeoutHighScore.text = scoreComponent.highScore.ToString();
    }

}
