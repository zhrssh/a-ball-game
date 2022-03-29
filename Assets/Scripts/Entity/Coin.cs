using UnityEngine;
using DevZhrssh.Managers.Components;
public class Coin : Entity
{
    private ScoreComponent scoreComponent;

    protected override void Start()
    {
        base.Start();
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
    }

    // This is the class that gives the player points
    public override void OnPlayerCollide(GameObject other)
    {
        // TODO: Add score to player
        scoreComponent.AddScore(10); // Gives one point to player
        base.OnPlayerCollide(other);
    }
}
