using UnityEngine;
using DevZhrssh.Managers;

public class OnGamePaused : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController playerController;

    private void Start()
    {
        if (gameManager == null)
            gameManager = GameObject.FindObjectOfType<GameManager>();

        if (playerController == null)
            playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (gameManager.isGamePaused)
            playerController.enabled = false;
        else
            playerController.enabled = true;
    }
}
