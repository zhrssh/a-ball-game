using UnityEngine;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;

public class Entity : PooledObject, IDamageable
{
    [SerializeField] protected ParticleSystem particleOnCollide;
    [SerializeField] protected string audioOnCollide;

    // Audio manager
    private AudioManager audioManager;

    protected virtual void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public override void OnObjectReuse()
    {
        // TODO: Animations
    }

    public virtual void OnPlayerCollide(GameObject other)
    {
        Instantiate(particleOnCollide, transform.position, Quaternion.identity);
        audioManager.Play(audioOnCollide);
        gameObject.SetActive(false);
    }
}
