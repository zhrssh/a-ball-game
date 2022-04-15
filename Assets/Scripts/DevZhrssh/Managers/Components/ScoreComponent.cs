using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevZhrssh.Managers.Components
{
    public class ScoreComponent : MonoBehaviour
    {
        private PlayerDeathComponent playerDeathComponent;

        private void Start()
        {
            playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();
            playerDeathComponent.onPlayerDeathCallback += CheckHighScore; // checks high score everytime the player dies

            ResetScore();
        }

        // Scoring System
        private int _playerScore = 0;
        public int playerScore
        {
            get { return _playerScore; }
            set { _playerScore = value; }
        }

        private int _highScore = 0;
        public int highScore
        {
            get { return _highScore; }
            set { _highScore = value; }
        }

        private int _scoreMultiplier = 1;
        public int scoreMultiplier
        {
            get { return _scoreMultiplier; }
            set { _scoreMultiplier = value; }
        }

        // Public Functions

        public void AddScore(int scoreToAdd)
        {
            // Add score to the current score
            _playerScore = _playerScore + ( scoreToAdd * _scoreMultiplier );
        }

        public void ResetScore()
        {
            // Resets the score back to zero
            _playerScore = 0;
        }

        public void CheckHighScore()
        {
            if (_playerScore > _highScore)
                _highScore = _playerScore;
            else
                return;
        }
    }
}
