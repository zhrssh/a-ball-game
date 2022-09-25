using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;

public class AdsHandler : MonoBehaviour
{
    private AdsManager adsManager;
    private GameSystemSaveHandler playerSaveHandler;

    private PlayerDeathComponent playerDeathComponent;

    private int playCount;
    public bool showAds { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();
        playerDeathComponent.onPlayerDeathCallback += PlayAdOnFifthDeath;

        adsManager = AdsManager.Instance;

        playerSaveHandler = GameObject.FindObjectOfType<GameSystemSaveHandler>();

        playCount = playerSaveHandler.playCount;

        showAds = playerSaveHandler.showAds;
    }

    public void PlayAdOnFifthDeath()
    {
        if (showAds == false) return;
        if (playCount % 5 == 0 && playCount != 0)
        {
            adsManager.PlayAd();
        }
    }

    public void RemoveAds()
    {
        showAds = false;
    }
}
