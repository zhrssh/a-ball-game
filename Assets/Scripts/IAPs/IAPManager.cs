using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private string purchaseAll = "com.zherish.aballgame.purchaseall";

    // Button
    [SerializeField] private GameObject purchaseAllButton;

    // Shop
    private GameSystemShop shop;

    private void Awake()
    {
        // References
        shop = GameObject.FindObjectOfType<GameSystemShop>();
       
    }

    private void Start()
    {
        // Check if there is internet connection, if there isn't disable unlock button
        if (Application.internetReachability == NetworkReachability.NotReachable)
            purchaseAllButton.SetActive(false);
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == purchaseAll)
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

    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        
    }

    public void OnInitializeFailed(InitializationFailureReason reason)
    {
        
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        // Processes purchase of each product
        if (e.purchasedProduct.hasReceipt == true)
        {
            // Restores IAP products
            OnPurchaseComplete(e.purchasedProduct);
            return PurchaseProcessingResult.Complete;
        }
        else
            return PurchaseProcessingResult.Pending;
    }
}
