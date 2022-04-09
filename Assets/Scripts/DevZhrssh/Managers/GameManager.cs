using UnityEngine;
using UnityEngine.SceneManagement;
using DevZhrssh.Managers.Components;
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

        public bool isGamePaused;

        // Player Death Component
        private PlayerDeathComponent playerDeathComponent;

        private void Start()
        {
            // Hides the panels at the start of the game
            loadingScreen?.SetActive(false);
            pauseScreen?.SetActive(false);
            deathScreen?.SetActive(false);

            var obj = GameObject.FindObjectOfType<PlayerDeathComponent>();
            if (obj != null)
                playerDeathComponent = obj as PlayerDeathComponent;

            // Checks if null, then subscribe to the event in death component
            if (playerDeathComponent != null)
                playerDeathComponent.onPlayerDeathCallback += ShowDeathScreen;
        }

        public void ShowDeathScreen()
        {
            deathScreen.SetActive(true);
        }

        public void HideDeathScreen()
        {
            deathScreen.SetActive(false);
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

        public void PauseGame()
        {
            isGamePaused = true;
            Time.timeScale = 0;
            pauseScreen?.SetActive(true);
        }

        public void ResumeGame()
        {
            isGamePaused = false;
            Time.timeScale = 1;
            pauseScreen?.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
