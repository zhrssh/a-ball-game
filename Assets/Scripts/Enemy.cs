using UnityEngine;
using DevZhrssh.Singletons;
using DevZhrssh.Utilities;

public class Enemy : PooledObject, IDamageable
{
    [SerializeField] private ParticleSystem deathEffect;

    public virtual void OnPlayerCollide()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        AudioManager.instance.Play("Death");
        gameObject.SetActive(false);
    }
}
