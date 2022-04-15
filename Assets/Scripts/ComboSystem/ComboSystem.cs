using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.Managers.Components;

public class ComboSystem : MonoBehaviour
{
    // Time before combo expires
    public int comboLimit;
    public float comboDuration;
    public bool isComboing;

    // UI Zoom
    [SerializeField] private UIZoom zoom;

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

    // Reference to player
    private PlayerCollision player;

    // Reference to score
    private ScoreComponent scoreComponent;
   

    private void Start()
    {
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        player = GameObject.FindObjectOfType<PlayerCollision>();
    }

    private void Update()
    {
        if (isComboing)
        {
            if (_comboTime > 0)
            {
                // Combo
                scoreComponent.scoreMultiplier = _currentCombo; // sets the score multiplier
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

        // Adds a limit to the combo
        if (_currentCombo < comboLimit)
            _currentCombo++;

        // Resets the combo time every hit of entity
        _comboTime = comboDuration;

        // Activate effect
        StartCoroutine(zoom.StartZoom());
    }


}
