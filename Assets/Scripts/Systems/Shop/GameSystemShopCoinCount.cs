using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemShopCoinCount : MonoBehaviour
{
    [SerializeField] private int currentCoinCount = 0;
    private int multiplier = 1;

    public void SetMultiplier(int multiplier)
    {
        this.multiplier = multiplier;
    }

    public void ResetMultiplier()
    {
        this.multiplier = 1;
    }

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
        currentCoinCount = currentCoinCount + (amount * multiplier);
    }

    public void SubtractCoin(int amount)
    {
        currentCoinCount = currentCoinCount - amount;
    }
}
