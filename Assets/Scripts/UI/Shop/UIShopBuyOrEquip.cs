using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.SaveSystem;
using TMPro;

public class UIShopBuyOrEquip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    private int _currentEquippedBall;

    // UI
    private UIShopBall _currentSelectedBall;

    // Game System Shop
    private GameSystemShop shop;
    private GameSystemShopCoinCount coinCount;

    private void Awake()
    {
        // References
        shop = GameObject.FindObjectOfType<GameSystemShop>();
        coinCount = GameObject.FindObjectOfType<GameSystemShopCoinCount>();
    }

    private void Start()
    {
        if (shop != null)
            _currentEquippedBall = shop.currentBallEquipped;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if (_currentSelectedBall != null && buttonText != null)
        {
            if (_currentSelectedBall.IsBought() == false)
                buttonText.text = _currentSelectedBall.GetPrice().ToString();

            if (_currentSelectedBall.IsBought() == true)
            {
                buttonText.text = "Equip";
            }

            if (_currentSelectedBall.GetBallID() == _currentEquippedBall)
            {
                buttonText.text = "Equipped";
            }

        }
    }

    public void BuyOrEquip()
    {
        if (coinCount.GetCoinCount() >= _currentSelectedBall.GetPrice() && _currentSelectedBall.IsBought() == false)
        {
            coinCount.SubtractCoin(_currentSelectedBall.GetPrice());
            shop.Buy(_currentSelectedBall.GetBallID());
            shop.Equip(_currentSelectedBall.GetBallID());
        }
        else if (_currentSelectedBall.IsBought() == true)
        {
            shop.Equip(_currentSelectedBall.GetBallID());
            _currentEquippedBall = _currentSelectedBall.GetBallID();
        }
    }

    public void SetCurrentBall(UIShopBall currentBall)
    {
        _currentSelectedBall = currentBall;
    }
}
