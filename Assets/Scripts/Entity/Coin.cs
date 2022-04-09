using UnityEngine;
using DevZhrssh.Managers.Components;
public class Coin : Entity
{
    [SerializeField] private int coinAmount = 1;
    private CoinCount coinCountScript;

    protected override void Start()
    {
        base.Start();
        coinCountScript = GameObject.FindObjectOfType<CoinCount>();
    }

    // This is the class that gives the player points
    public override void OnPlayerCollide(GameObject other)
    {
        // TODO: Add score to player
        coinCountScript.AddCoin(coinAmount);
        base.OnPlayerCollide(other);
    }
}
