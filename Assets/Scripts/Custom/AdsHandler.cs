using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;

public class AdsHandler : MonoBehaviour
{
    private AdsManager adsManager;
    private PlayerSaveHandler playerSaveHandler;

    private PlayerDeathComponent playerDeathComponent;

    private int playCount;

    // Start is called before the first frame update
    void Start()
    {
        playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();
        playerDeathComponent.onPlayerDeathCallback += PlayAdOnFifthDeath;

        adsManager = AdsManager.Instance;
        playerSaveHandler = GameObject.FindObjectOfType<PlayerSaveHandler>();

        playCount = playerSaveHandler.playCount;
    }

    public void PlayAdOnFifthDeath()
    {
        Debug.Log("Play Count: " + playCount);
        if (playCount % 5 == 0 && playCount != 0)
        {
            adsManager.PlayAd();
        }
    }
}
