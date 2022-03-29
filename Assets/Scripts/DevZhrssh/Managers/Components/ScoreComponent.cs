using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevZhrssh.Managers.Components
{
    public class ScoreComponent : MonoBehaviour
    {
        // Can be attached to Game Manager
        private GameManager gameManager;

        private void Start()
        {
            gameManager = GetComponent<GameManager>();
        }

        // Scoring System
        private int _playerScore = 0;
        public int playerScore
        {
            get { return _playerScore; }
        }
        
        public void AddScore(int scoreToAdd)
        {
            // Add score to the current score
            _playerScore = _playerScore + scoreToAdd;
        }

        public void ResetScore()
        {
            // Resets the score back to zero
            _playerScore = 0;
        }
    }
}
