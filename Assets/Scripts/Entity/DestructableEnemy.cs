using UnityEngine;
using DevZhrssh.Managers.Components;

public class DestructableEnemy : Entity
{
    [SerializeField] private int score = 1000;
    private ScoreComponent scoreComponentScript;

    protected override void Start()
    {
        base.Start();
        scoreComponentScript = GameObject.FindObjectOfType<ScoreComponent>();
    }

    public override void OnPlayerCollide(GameObject other)
    {
        scoreComponentScript.AddScore(score);
        base.OnPlayerCollide(other);
    }
}
