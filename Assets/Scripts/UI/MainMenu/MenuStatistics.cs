using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.SaveSystem;
using TMPro;

public class MenuStatistics : MonoBehaviour
{
    // Start is called before the first frame update
    private SaveSystem saveSystem;
    private SaveData data;

    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI playCount;
    [SerializeField] private TextMeshProUGUI deaths;


    void Start()
    {
        saveSystem = FindObjectOfType<SaveSystem>();
        if (saveSystem == null)
        {
            Debug.LogError("No Save System!");
            return;
        }

        data = saveSystem.Load();

        if (data != null)
        {
            highScore.text = "High Score: " + data.highScore;
            playCount.text = "Plays: " + data.playCount;
            deaths.text = "Deaths: " + data.deaths;
        }
    }
}
