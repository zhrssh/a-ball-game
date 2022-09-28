using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.Managers;
using UnityEngine.UI;

public class AdsHandler : MonoBehaviour
{
    // Managers
    private GameManager gameManager;
    private AdsManager adsManager;
    private AdsReward adsReward;

    // Checks if player has bought IAP
    private GameSystemShop shop;

    // Checks if player has 5 death counts
    private GameStatisticsSaveHandler gameStatistics;

    public bool showAds { get; private set; }

    // Disable play button temporarily when play ad is called
    [SerializeField] private Button playButton;

    private void Awake()
    {
        // References
        gameManager = GameObject.FindObjectOfType<GameManager>();
        adsManager = AdsManager.Instance;
        gameStatistics = GameObject.FindObjectOfType<GameStatisticsSaveHandler>();
        shop = GameObject.FindObjectOfType<GameSystemShop>();
        adsReward = GameObject.FindObjectOfType<AdsReward>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // If player has not bought IAP, we show ads
        showAds = (shop.hasBoughtIAP == true) ? false : true;

        if (gameManager != null)
            gameManager.onGameEndCallback += PlayAdOnFifthDeath;

        if (adsManager != null)
        {
            adsManager.onAdLoadStartCallback += DisablePlayButton;
            adsManager.onAdLoadEndCallback += EnablePlayButton;
        }    
    }

    public void PlayAdOnFifthDeath()
    {
        if (showAds == true)
        {
            if (gameStatistics.playCount % 5 == 0 && gameStatistics.playCount != 0 && adsReward.hasRecentlyRewarded == false)
            {
                adsManager.PlayAd();
            }
        }
    }

    private void EnablePlayButton()
    {
        if (playButton != null)
            playButton.interactable = true;
    }

    private void DisablePlayButton()
    {
        if (playButton != null)
            playButton.interactable = false;
    }
}
