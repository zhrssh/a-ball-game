using UnityEngine;
using TMPro;
using DevZhrssh.Managers.Components;
using UnityEngine.UI;

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

    // Combo
    [SerializeField] private RectTransform comboProgressBar;
    [SerializeField] private TextMeshProUGUI currentCombo;
    private ComboSystem comboSystem;

    private void Start()
    {
        comboSystem = GameObject.FindObjectOfType<ComboSystem>();
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

        // Timer
        if (time != null)
            time.text = Mathf.FloorToInt(timeComponent.currentTime).ToString();

        // Coincount
        if (coinCount != null)
            coinCount.text = coinCountScript.GetCoinCount().ToString();

        // Combosystem
        if (comboProgressBar != null)
        {
            float percent = comboSystem.comboTime / comboSystem.comboDuration;
            comboProgressBar.localScale = new Vector3(percent, comboProgressBar.localScale.y, comboProgressBar.localScale.z);
        }

        if (currentCombo != null)
        {
            currentCombo.text = (comboSystem.currentCombo > 1) ? "x" + comboSystem.currentCombo : "";
        }

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
