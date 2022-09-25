using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemShopCoinCount : MonoBehaviour
{
    [SerializeField] private int currentCoinCount = 0;
    public int GetCoinCount()
    {
        return currentCoinCount;
    }

    public void SetCointCount(int value)
    {
        currentCoinCount = value;
    }

    public void AddCoin(int amount)
    {
        currentCoinCount = currentCoinCount + amount;
    }

    public void SubtractCoin(int amount)
    {
        currentCoinCount = currentCoinCount - amount;
    }
}
