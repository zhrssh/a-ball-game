using UnityEngine;
using TMPro;
using DevZhrssh.Managers.Components;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    private ScoreComponent scoreComponent;

    private void Start()
    {
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
    }

    private void Update()
    {
        // Update score
        if (score != null)
            score.text = scoreComponent.playerScore.ToString();
    }

}
