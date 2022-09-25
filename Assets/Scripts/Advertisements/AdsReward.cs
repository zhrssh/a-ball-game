using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.Managers;

public class AdsReward : MonoBehaviour
{
    private AdsManager adsManager;

    private void Start()
    {
        adsManager = AdsManager.Instance;
        if (adsManager != null)
        {
            adsManager.onAdCompletedCallback += RewardPlayer;
        }
    }

    private void RewardPlayer()
    {
        // Rewards player with additional life
        // Rewards player with 10 seconds random buff
        Debug.Log("Reward PLAYER!!!");
    }
}
