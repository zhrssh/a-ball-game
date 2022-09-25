using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DevZhrssh.SaveSystem;

public class MenuDisplayBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image display;
    [SerializeField] private Image[] balls;

    private PlayerSaveHandler playerSave;
    private SaveData data;

    void Start()
    {
        playerSave = GameObject.FindObjectOfType<PlayerSaveHandler>();
        if (playerSave != null)
            data = playerSave.Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (data != null)
        {
            int currentBall = data.currentEquippedBall;
            if (balls.Length > 0)
            {
                display.sprite = balls[currentBall].sprite;
                display.color = balls[currentBall].color;
            }
        }
        else
        {
            UpdateData();
        }
    }

    public void UpdateData()
    {
        data = playerSave.Load();
    }
}
