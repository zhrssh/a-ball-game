using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ShopBallBuyAndEquip : MonoBehaviour
{
    public delegate void OnButtonPressed(int id);
    public event OnButtonPressed OnButtonPressedCallback;

    [SerializeField] private CoinCount coinCount;
    [SerializeField] private UICoinCount coinCountUI;

    [SerializeField] private TextMeshProUGUI buttonText;

    private ShopBallSelect currentSelectedBall;

    private int currentEquippedBall;

    // Update is called once per frame
    void Update()
    {
        buttonText.text = currentSelectedBall.GetPrice().ToString();
        UpdateText();
    }

    private void UpdateText()
    {
        if (currentSelectedBall.IsBought() == true)
        {
            buttonText.text = "Equip";
        }

        if (currentSelectedBall.GetBallID() == currentEquippedBall)
        {
            buttonText.text = "Equipped";
        }
    }

    public void BuyOrEquip()
    {
        if (coinCount.GetCoinCount() >= currentSelectedBall.GetPrice() && currentSelectedBall.IsBought() == false)
        {
            if (OnButtonPressedCallback != null)
                OnButtonPressedCallback.Invoke(currentSelectedBall.GetBallID());
            coinCount.SubtractCoin(currentSelectedBall.GetPrice());
            coinCountUI.UpdateCount();
            currentEquippedBall = currentSelectedBall.GetBallID();
        }
        else if (currentSelectedBall.IsBought() == true)
        {
            currentEquippedBall = currentSelectedBall.GetBallID();
        }
    }

    public void SetCurrentBall(ShopBallSelect currentBall)
    {
        currentSelectedBall = currentBall;
    }
}
