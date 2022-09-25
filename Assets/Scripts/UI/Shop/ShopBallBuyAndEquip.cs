using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.SaveSystem;
using TMPro;

public class ShopBallBuyAndEquip : MonoBehaviour
{
    public delegate void OnButtonPressed(int id);
    public event OnButtonPressed OnButtonPressedCallback;

    [SerializeField] private CoinCount coinCount;

    [SerializeField] private TextMeshProUGUI buttonText;

    private ShopBallSelect _currentSelectedBall;
    public ShopBallSelect currentSelectedBall { get { return _currentSelectedBall; } }

    private int _currentEquippedBall;
    public int currentEquippedBall { get { return _currentEquippedBall; } }

    private List<int> _boughtItemsList;
    public List<int> boughtItemsList { get { return _boughtItemsList; } }

    private PlayerSaveHandler playerSave;
    private SaveData data;

    private void Start()
    {
        _boughtItemsList = new List<int>();
        playerSave = GameObject.FindObjectOfType<PlayerSaveHandler>();
        if (playerSave != null)
            data = playerSave.Load();

        _currentEquippedBall = data.currentEquippedBall;

        CheckBoughtItems();
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
            buttonText.text = _currentSelectedBall.GetPrice().ToString();

            if (_currentSelectedBall.IsBought() == true)
            {
                buttonText.text = "Equip";
            }

            if (_currentSelectedBall.GetBallID() == currentEquippedBall)
            {
                buttonText.text = "Equipped";
            }
        }
    }

    public void BuyOrEquip()
    {
        if (coinCount.GetCoinCount() >= _currentSelectedBall.GetPrice() && _currentSelectedBall.IsBought() == false)
        {
            if (OnButtonPressedCallback != null)
                OnButtonPressedCallback.Invoke(_currentSelectedBall.GetBallID());
            coinCount.SubtractCoin(_currentSelectedBall.GetPrice());
            _currentEquippedBall = _currentSelectedBall.GetBallID();

            CheckBoughtItems();
        }
        else if (_currentSelectedBall.IsBought() == true)
        {
            _currentEquippedBall = _currentSelectedBall.GetBallID();
        }

        // Saves whenever the player presses the button
        if (playerSave != null)
            playerSave.Save();
    }

    public void SetCurrentBall(ShopBallSelect currentBall)
    {
        _currentSelectedBall = currentBall;
    }

    public void CheckBoughtItems()
    {
        ShopBallSelect[] balls = GameObject.FindObjectsOfType<ShopBallSelect>();

        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i].IsBought() == true)
            {
                if (_boughtItemsList.Contains(balls[i].GetBallID()) == false)
                    _boughtItemsList.Add(balls[i].GetBallID());
            }
        }
    }
}
