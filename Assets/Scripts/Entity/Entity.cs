using UnityEngine;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;

public class Entity : PooledObject, IDamageable
{
    [SerializeField] protected ParticleSystem particleOnCollide;
    [SerializeField] protected string audioOnCollide;

    // Audio manager
    private AudioManager audioManager;

    // Despawn Timer
    [SerializeField] protected float despawnTime = 5f;
    private float currentTime = 0;

    protected virtual void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    protected virtual void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > despawnTime)
        {
            gameObject.SetActive(false);
        }
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
