using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.SaveSystem;
using TMPro;

public class UIMainMenuStatistics : MonoBehaviour
{
    // Save Systems and Data
    private GameStatisticsSaveHandler saveHandler;
    private StatisticsData data;

    // Text Mesh UIs
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI playCount;
    [SerializeField] private TextMeshProUGUI unalives;

    private void Awake()
    {
        // References
        saveHandler = FindObjectOfType<GameStatisticsSaveHandler>();
        if (saveHandler == null)
        {
            Debug.LogError("No Save System!");
            return;
        }
    }

    void Start()
    {
        data = saveHandler.Load();

        if (data != null)
        {
            highScore.text = "High Score: " + data.hiscore;
            playCount.text = "Plays: " + data.playCount;
            unalives.text = "Unalives: " + data.unalives;
        }
    }
}
