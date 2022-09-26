using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.SaveSystem;
using TMPro;

public class UIShopBuyOrEquip : MonoBehaviour
{
    public delegate void OnButtonPressed(int id);
    public event OnButtonPressed OnButtonPressedCallback;

    [SerializeField] private GameSystemShopCoinCount coinCount;

    [SerializeField] private TextMeshProUGUI buttonText;

    private UIShopBall _currentSelectedBall;
    public UIShopBall currentSelectedBall { get { return _currentSelectedBall; } }

    private int _currentEquippedBall;
    public int currentEquippedBall { get { return _currentEquippedBall; } }

    private List<int> _boughtItemsList;
    public List<int> boughtItemsList { get { return _boughtItemsList; } }

    private GameSystemSaveHandler playerSave;
    private SaveData data;
    private SaveSystem saveSystem;

    private void Start()
    {
        _boughtItemsList = new List<int>();
        playerSave = GameObject.FindObjectOfType<GameSystemSaveHandler>();
        if (playerSave != null)
            data = playerSave.Load();

        saveSystem = GameObject.FindObjectOfType<SaveSystem>();
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
            if (_currentSelectedBall.IsBought() == false)
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
        else
        {
            playerSave = GameObject.FindObjectOfType<GameSystemSaveHandler>();
            playerSave.Save();
        }
    }

    public void SetCurrentBall(UIShopBall currentBall)
    {
        _currentSelectedBall = currentBall;
    }

    public void CheckBoughtItems()
    {
        UIShopBall[] balls = GameObject.FindObjectsOfType<UIShopBall>();

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