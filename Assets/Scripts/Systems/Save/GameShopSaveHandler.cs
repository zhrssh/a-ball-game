using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.SaveSystem;
using DevZhrssh.Managers;

public class GameShopSaveHandler : MonoBehaviour
{
    // Save System and Game manager
    private SaveSystem saveSystem;
    private GameManager gameManager;

    // Handles coin count
    private GameSystemShopCoinCount currency;

    // Handles balls bought
    private GameSystemShop shop;

    private void Awake()
    {
        // References
        shop = GameObject.FindObjectOfType<GameSystemShop>();

        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager != null)
            gameManager.onGameEndCallback += Save;

        currency = GameObject.FindObjectOfType<GameSystemShopCoinCount>();
        if (currency == null)
            Debug.LogError("No GameSystemShopCoinCount Found!");

        saveSystem = GameObject.FindObjectOfType<SaveSystem>();

        // Load save file
        Load();

    }

    private void Start()
    {

    }

    public ShopData Load()
    {
        if (saveSystem != null)
        {
            ShopData data = saveSystem.Load<ShopData>("shopdata");
            if (data != null)
            {
                currency.SetCointCount(data.currency);
                shop.RestorePurchases(data.hasBall2, data.hasBall3, data.hasBall4, data.hasBall5, data.hasBoughtIAP);
            }
            else
            {
                data = new ShopData();
            }

            return data;
        } 
        else
        {
            return null;
        }
    }

    public void Save()
    {
        if (saveSystem != null)
        {
            ShopData data = new ShopData(currency.GetCoinCount(), shop.ball2, shop.ball3, shop.ball4, shop.ball5, shop.hasBoughtIAP);
            saveSystem.Save(data, "shopdata");
        }
    }
}

[System.Serializable]
public class ShopData
{
    public int currency;
    public bool hasBall2;
    public bool hasBall3;
    public bool hasBall4;
    public bool hasBall5;
    public bool hasBoughtIAP;

    public ShopData()
    {
        currency = 0;
        hasBall2 = false;
        hasBall3 = false;
        hasBall4 = false;
        hasBall5 = false;
        hasBoughtIAP = false;
    }

    public ShopData(int currency, bool hasBall2, bool hasBall3, bool hasBall4, bool hasBall5, bool hasBoughtIAP)
    {
        this.currency = currency;
        this.hasBall2 = hasBall2;
        this.hasBall3 = hasBall3;
        this.hasBall4 = hasBall4;
        this.hasBall5 = hasBall5;
        this.hasBoughtIAP = hasBoughtIAP;
    }
}

