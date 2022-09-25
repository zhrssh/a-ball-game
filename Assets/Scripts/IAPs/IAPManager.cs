using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private string unlockAll = "com.zherish.aballgame.unlockall";

    private AdsHandler adsHandler;
    private ShopBallSelect[] balls;
    private PlayerSaveHandler playerSave;

    private void Start()
    {
        adsHandler = GameObject.FindObjectOfType<AdsHandler>();
        balls = GameObject.FindObjectsOfType<ShopBallSelect>();
        playerSave = GameObject.FindObjectOfType<PlayerSaveHandler>();
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == unlockAll)
        {
            // Remove Ads
            if (adsHandler != null)
                adsHandler.RemoveAds();

            // Unlock all balls
            if (balls.Length > 0)
            {
                for (int i = 0; i < balls.Length; i++)
                {
                    balls[i].Buy(i);
                }
            }

            // Save
            if (playerSave != null)
                playerSave.Save();
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + "failed purchase: " + reason);
    }
}
