using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemShop : MonoBehaviour
{
    // Game Shop Save Handler
    private GameShopSaveHandler saveHandler;

    // Game Player Save Handler
    private GamePlayerSaveHandler playerSaveHandler;

    // Handles amount of currency
    private GameSystemShopCoinCount currency;
    private int _currencyCount;
    public int currencyCount
    {
        get
        {
            return _currencyCount;
        }
    }

    private int _currentBallEquipped;
    private bool _ball2;
    private bool _ball3;
    private bool _ball4;
    private bool _ball5;
    private bool _hasBoughtIAP;

    public int currentBallEquipped { get { return _currentBallEquipped; } }
    public bool ball2 { get { return _ball2; } }
    public bool ball3 { get { return _ball3; } }
    public bool ball4 { get { return _ball4; } }
    public bool ball5 { get { return _ball5; } }
    public bool hasBoughtIAP { get { return _hasBoughtIAP; } }

    private void Awake()
    {
        // References
        currency = GameObject.FindObjectOfType<GameSystemShopCoinCount>();
        saveHandler = GameObject.FindObjectOfType<GameShopSaveHandler>();
        playerSaveHandler = GameObject.FindObjectOfType<GamePlayerSaveHandler>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _currencyCount = currency.GetCoinCount();

        if (playerSaveHandler != null)
        {
            _currentBallEquipped = playerSaveHandler.playerData.currentBallEquipped;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void RestorePurchases(bool _ball2, bool _ball3, bool _ball4, bool _ball5, bool _hasBoughtIAP)
    {
        this._ball2 = _ball2;
        this._ball3 = _ball3;
        this._ball4 = _ball4;
        this._ball5 = _ball5;
        this._hasBoughtIAP = _hasBoughtIAP;
    }

    public bool IsBallBought(int id)
    {
        if (id == 1)
            return _ball2;
        if (id == 2)
            return _ball3;
        if (id == 3)
            return _ball4;
        if (id == 4)
            return _ball5;

        return false;
    }

    public void BuyIAP()
    {
        if (saveHandler != null)
        {
            _ball2 = true;
            _ball3 = true;
            _ball4 = true;
            _ball5 = true;
            _hasBoughtIAP = true;

            saveHandler.Save();
        }
    }

    public void Buy(int id)
    {
        if (saveHandler != null)
        {
            if (id == 1)
                _ball2 = true;
            if (id == 2)
                _ball3 = true;
            if (id == 3)
                _ball4 = true;
            if (id == 4)
                _ball5 = true;

            saveHandler.Save();
        }
    }

    public void Equip(int id)
    {
        if (playerSaveHandler != null)
        {
            _currentBallEquipped = id;
            playerSaveHandler.Save();
        }
    }
}

