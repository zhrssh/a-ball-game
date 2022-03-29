using UnityEngine;
using UnityEngine.SceneManagement;

namespace DevZhrssh.Managers
{
    public class GameManager : MonoBehaviour
    {
        public void LoadNextScene()
        {
            // Loads next scene
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadScene(string name)
        {
            SceneManager.LoadSceneAsync(name);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
