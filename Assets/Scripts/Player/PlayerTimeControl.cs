using UnityEngine;
using DevZhrssh.Managers;

public class PlayerTimeControl : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        var obj = GameManager.FindObjectOfType<GameManager>();
        if (obj != null)
            gameManager = obj as GameManager;
    }

    public void SlowTime(float amount)
    {
        if (!gameManager.isGamePaused)
        {
            Time.timeScale = amount;
            Time.fixedDeltaTime = Time.timeScale * 0.02f; // avoids game looking like it's lagging
        }
    }

    public void EndSlowTime()
    {
        if (!gameManager.isGamePaused)
            Time.timeScale = 1f;
    }
}
