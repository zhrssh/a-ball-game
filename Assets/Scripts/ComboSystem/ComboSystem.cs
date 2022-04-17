using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.Managers.Components;
using DevZhrssh.Managers;

public class ComboSystem : MonoBehaviour
{
    // Time before combo expires
    public int comboLimit = 12;
    public float comboDuration;
    public bool isComboing;

    // UI Zoom
    [SerializeField] private UIZoom zoom;

    // Game Manager
    private GameManager gameManager;

    public int comboMultiplier; // Used by powerups

    private int _currentCombo;
    public int currentCombo
    {
        get { return _currentCombo; }
    }

    private float _comboTime;
    public float comboTime
    {
        get { return _comboTime; }
    }

    // Reference to score
    private ScoreComponent scoreComponent;
   

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
    }

    private void Update()
    {
        if (gameManager.isGamePaused || gameManager.hasGameEnded) return; // stops comboing if game is paused or ended
        if (isComboing)
        {
            if (_comboTime > 0)
            {
                // Combo
                scoreComponent.scoreMultiplier = _currentCombo * ((comboMultiplier > 1) ? comboMultiplier : 1); // sets the score multiplier
                _comboTime -= Time.unscaledDeltaTime;
            }
            else
            {
                // if combo time runs out
                scoreComponent.scoreMultiplier = 1; // resets the multiplier
                _currentCombo = 0; // resets the combo
                isComboing = false;
            }
        }
    }

    public void StartCombo()
    {
        // Resets time every combo, adds combo counter
        if (!isComboing)
            isComboing = true;

        // Adds combo
        if (_currentCombo < comboLimit)
            _currentCombo++;

        // Resets the combo time every hit of entity
        _comboTime = comboDuration;

        // Activate effect
        StartCoroutine(zoom.StartZoom());
    }


}
