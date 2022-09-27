using UnityEngine;
using UnityEngine.SceneManagement;
using DevZhrssh.Managers.Components;
using DevZhrssh.SaveSystem;
using System.Collections;
using System;

namespace DevZhrssh.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject deathScreen;
        [SerializeField] private GameObject timeoutScreen;

        public bool isGamePaused;
        public bool hasGameEnded;

        // Save System
        private SaveSystem.SaveSystem saveSystem;

        // Time Manager
        public bool hasTimeManager { get; private set; }
        private TimeManager timeManager;

        // Player Death Component
        public bool hasDeathComponent { get; private set; }
        private PlayerDeathComponent playerDeathComponent;

        // Delegates
        public delegate void OnGameStart();
        public event OnGameStart onGameStartCallback;

        public delegate void OnGamePause();
        public event OnGamePause onGamePauseCallback;

        public delegate void OnGameUnpause();
        public event OnGameUnpause onGameUnpauseCallback;

        public delegate void OnGameEnd();
        public event OnGameEnd onGameEndCallback;

        private void Start()
        {
            // Calls all functions subscribed to the event
            if (onGameStartCallback != null)
                onGameStartCallback.Invoke();

            // Hides the panels at the start of the game
            loadingScreen?.SetActive(false);
            pauseScreen?.SetActive(false);
            deathScreen?.SetActive(false);
            timeoutScreen?.SetActive(false);

            // Player Death Component
            playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();
            if (playerDeathComponent != null)
            {
                hasDeathComponent = true;
                playerDeathComponent.onPlayerDeathCallback += PlayerHasDied; // Subscribe to player death component
            }

            timeManager = GameObject.FindObjectOfType<TimeManager>();
            if (timeManager != null)
            {
                hasTimeManager = true;
                timeManager.OnTimeReachedZeroCallback += OnTimeRunOut; // Subscribes if the game is timed
            }
        }

        public void LoadNextScene()
        {
            // Loads next scene
            StartCoroutine(OnLoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        }

        public void LoadScene(string name)
        {
            StartCoroutine(OnLoadScene(name));
        }

        private IEnumerator OnLoadScene(string name)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(name);

            while (!operation.isDone)
            {
                loadingScreen.SetActive(true); // enable while loading
                yield return null;
            }

            loadingScreen.SetActive(false); // disable loading screen after scene is loaded
        }

        private IEnumerator OnLoadScene(int index)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);

            while (!operation.isDone)
            {
                loadingScreen.SetActive(true); // enable while loading
                yield return null;
            }

            loadingScreen.SetActive(false); // disable loading screen after scene is loaded
        }

        private void GameHasEnded()
        {
            // Calls all function subscribed to the end game callback
            hasGameEnded = true;
            Time.timeScale = 0;
            if (onGameEndCallback != null)
                onGameEndCallback.Invoke();
        }

        private void GameHasPaused()
        {
            isGamePaused = true;
            Time.timeScale = 0;
            if (onGamePauseCallback != null)
                onGamePauseCallback.Invoke();
        }

        private void GameHasUnpaused()
        {
            isGamePaused = false;
            Time.timeScale = 1;
            if (onGameUnpauseCallback != null)
                onGameUnpauseCallback.Invoke();
        }

        // Public functions to use for other classes
        public void PlayerHasDied()
        {
            deathScreen.SetActive(true);
            GameHasEnded();
        }

        public void OnTimeRunOut()
        {
            timeoutScreen.SetActive(true);
            GameHasEnded();
        }

        public void PauseGame()
        {
            pauseScreen?.SetActive(true);
            GameHasPaused();
        }

        public void ResumeGame()
        {
            pauseScreen?.SetActive(false);
            GameHasUnpaused();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
