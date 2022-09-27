using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.SaveSystem;

public class GamePlayerSaveHandler : MonoBehaviour
{
    // Save System
    private SaveSystem saveSystem;

    // Shop
    private GameSystemShop shop;

    private PlayerData _playerData;
    public PlayerData playerData { get { return _playerData; } }

    private void Awake()
    {
        // References
        saveSystem = GameObject.FindObjectOfType<SaveSystem>();
        shop = GameObject.FindObjectOfType<GameSystemShop>();

        Load();
    }

    private void Start()
    {

    }

    private void Load()
    {
        if (saveSystem != null)
        {
            _playerData = saveSystem.Load<PlayerData>("playerdata");
            if (_playerData == null)
            {
                _playerData = new PlayerData();
            }
        }
    }

    public void Save()
    {
        if (saveSystem != null)
        {
            _playerData = new PlayerData(shop.currentBallEquipped);
            saveSystem.Save(_playerData, "playerdata");
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public int currentBallEquipped;

    public PlayerData()
    {
        currentBallEquipped = 0;
    }

    public PlayerData(int currentBallEquipped)
    {
        this.currentBallEquipped = currentBallEquipped;
    }
}
