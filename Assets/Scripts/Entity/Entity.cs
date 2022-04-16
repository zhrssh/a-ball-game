using UnityEngine;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;
using System.Collections;

public class Entity : PooledObject, IDamageable
{
    // Audio manager
    private AudioManager audioManager;

    // Despawn Timer
    [SerializeField] protected float despawnTime = 5f;
    [SerializeField] protected float indicatorDespawnTime = 1f;
    private float currentTime = 0;

    // Score System
    private ScoreComponent scoreComponent;

    // Currency System
    private CoinCount coinCount;

    // Combo System
    private ComboSystem comboSystem;

    // Entity Type
    public EntityClass entityClass;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    protected virtual void Start()
    {
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        comboSystem = GameObject.FindObjectOfType<ComboSystem>();
        coinCount = GameObject.FindObjectOfType<CoinCount>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = entityClass.sprite;
            spriteRenderer.color = entityClass.color;
        }

        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            circleCollider.radius = entityClass.sprite.bounds.size.magnitude / 6f;
        }
    }

    protected virtual void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > despawnTime)
        {
            // Spawns particles based on entity color
            ParticleSystem particles = Instantiate(entityClass.particles, transform.position, Quaternion.identity);
            ParticleSystem.MainModule ma = particles.main;
            ma.startColor = entityClass.color;

            gameObject.SetActive(false);
        }
    }

    public override void OnObjectReuse(Vector3 position, Quaternion rotation)
    {
        
    }

    public virtual void OnPlayerCollide(GameObject other)
    {
        switch (entityClass.entityType)
        {
            case EntityClass.EntityType.Collectible:
                scoreComponent.AddScore(entityClass.score);
                coinCount.AddCoin(entityClass.coinAmount);
                break;
            case EntityClass.EntityType.Damageable:
                scoreComponent.AddScore(entityClass.score);
                break;
            case EntityClass.EntityType.Deadly:
                other.GetComponent<Player>()?.Death();
                break;
            default:
                break;
        }
        
        // Spawns particles based on entity color
        ParticleSystem particles = Instantiate(entityClass.particles, transform.position, Quaternion.identity);
        ParticleSystem.MainModule ma = particles.main;
        ma.startColor = entityClass.color;

        audioManager.Play(entityClass.audioOnCollide);

        // When the player is still alive and active we start combo
        if (other.activeSelf == true)
        {
            comboSystem.StartCombo(); // Starts the combo or add to combo
        }

        gameObject.SetActive(false);
    }
}
