using UnityEngine;
using TMPro;
using DevZhrssh.Managers.Components;

public class DisplayStats : MonoBehaviour
{
    // Score
    [SerializeField] private TextMeshProUGUI score;
    private ScoreComponent scoreComponent;

    // Time
    [SerializeField] private TextMeshProUGUI time;
    private TimeComponent timeComponent;

    private void Start()
    {
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        timeComponent = GameObject.FindObjectOfType<TimeComponent>();
    }

    private void Update()
    {
        // Update score
        if (score != null)
            score.text = scoreComponent.playerScore.ToString();

        if (time != null)
            time.text = Mathf.FloorToInt(timeComponent.currentTime).ToString();
    }

}
