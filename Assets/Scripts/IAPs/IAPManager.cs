using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private string unlockAll = "com.zherish.aballgame.unlockall";

    // Shop
    private GameSystemShop shop;

    private void Awake()
    {
        // References
        shop = GameObject.FindObjectOfType<GameSystemShop>();
    }

    private void Start()
    {

    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == unlockAll)
        {
            if (shop != null)
            {
                // Remove Ads and Unlock All Balls
                shop.BuyIAP();
            }
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + "failed purchase: " + reason);
    }
}
