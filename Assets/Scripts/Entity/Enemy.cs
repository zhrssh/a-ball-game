using UnityEngine;

public class Enemy : Entity
{
    public override void OnPlayerCollide(GameObject other)
    {
        // Disable player
        other.GetComponent<PlayerCollision>()?.Death();
        base.OnPlayerCollide(other);
    }
}
