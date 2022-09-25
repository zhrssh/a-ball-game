using UnityEngine;
using TMPro;
using DevZhrssh.Managers.Components;
using UnityEngine.UI;

public class UIGameDisplayStatistics : MonoBehaviour
{
    // Score
    [SerializeField] private TextMeshProUGUI gameScore;
    [SerializeField] private TextMeshProUGUI deathScore;
    [SerializeField] private TextMeshProUGUI highScore;

    private ScoreComponent scoreComponent;

    // Currency
    [SerializeField] private TextMeshProUGUI coinCount;
    private GameSystemShopCoinCount coinCountScript;

    // Time
    [SerializeField] private TextMeshProUGUI time;
    private TimeComponent timeComponent;

    // Combo
    [SerializeField] private RectTransform comboProgressBar;
    [SerializeField] private TextMeshProUGUI currentCombo;
    private GameSystemCombo comboSystem;

    private void Start()
    {
        comboSystem = GameObject.FindObjectOfType<GameSystemCombo>();
        coinCountScript = GameObject.FindObjectOfType<GameSystemShopCoinCount>();
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        timeComponent = GameObject.FindObjectOfType<TimeComponent>();
    }

    private void Update()
    {
        DisplayHUD(); // Gameplay HUD
        DeathHUD();
    }

    private void DisplayHUD()
    {
        // Update score
        if (gameScore != null)
            gameScore.text = scoreComponent.playerScore.ToString();

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
            if (comboSystem.currentCombo > 0)
                comboProgressBar.GetComponent<Image>().color = Color.green;

            if (comboSystem.currentCombo > 3)
                comboProgressBar.GetComponent<Image>().color = Color.yellow;

            if (comboSystem.currentCombo > 7)
                comboProgressBar.GetComponent<Image>().color = Color.red;

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
            deathScore.text = "Score: " + scoreComponent.playerScore.ToString();

        if (highScore != null)
            highScore.text = "High Score: " + scoreComponent.highScore.ToString();
    }
}
