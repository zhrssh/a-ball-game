using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DevZhrssh.SaveSystem;

public class UIMainMenuBallDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image display;
    [SerializeField] private Image[] balls;

    // Handles current equippedball
    private GamePlayerSaveHandler playerSave;
    private PlayerData playerData;

    private void Awake()
    {
        playerSave = GameObject.FindObjectOfType<GamePlayerSaveHandler>();
    }

    void Start()
    {
        if (playerSave != null)
            playerData = playerSave.playerData;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerData != null)
        {
            int currentBall = playerData.currentBallEquipped;
            if (balls.Length > 0)
            {
                display.sprite = balls[currentBall].sprite;
                display.color = balls[currentBall].color;
            }
        }
    }
}
