using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;

public class AdsHandler : MonoBehaviour
{
    private AdsManager adsManager;

    // Checks if player has bought IAP
    private GameSystemShop shop;

    // Checks if player has 5 death counts
    private GameStatisticsSaveHandler gameStatistics;

    public bool showAds { get; private set; }

    private void Awake()
    {
        // References
        adsManager = AdsManager.Instance;
        gameStatistics = GameObject.FindObjectOfType<GameStatisticsSaveHandler>();
        shop = GameObject.FindObjectOfType<GameSystemShop>();
    }

    // Start is called before the first frame update
    void Start()
    {
        showAds = shop.hasBoughtIAP;
    }

    public void PlayAdOnFifthDeath()
    {
        if (showAds == false) return;
        if (gameStatistics.playCount % 5 == 0 && gameStatistics.playCount != 0)
        {
            adsManager.PlayAd();
        }
    }

    public void RemoveAds()
    {
        showAds = false;
    }
}
