using UnityEngine;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;
using System.Collections;

public class EntityObject : PooledObject, IDamageable
{
    // Audio manager
    private AudioManager audioManager;

    // Despawn Timer
    [SerializeField] protected float despawnTime = 5f;
    private float currentTime = 0;

    // Score System
    private ScoreComponent scoreComponent;

    // Currency System
    private GameSystemShopCoinCount coinCount;

    // Combo System
    private GameSystemCombo comboSystem;

    // Entity Type
    public EntityClass entityClass;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    protected virtual void Start()
    {
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        comboSystem = GameObject.FindObjectOfType<GameSystemCombo>();
        coinCount = GameObject.FindObjectOfType<GameSystemShopCoinCount>();

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
            ParticleSystem particles = Instantiate(entityClass.particlesOnCollide, transform.position, Quaternion.identity);
            ParticleSystem.MainModule ma = particles.main;
            ma.startColor = entityClass.color;

            gameObject.SetActive(false);
        }
    }

    public override void OnObjectReuse(Vector3 position, Quaternion rotation)
    {
        // Resets despawn timer
        currentTime = 0;

        // Reset, if entity is turret ai
        if (GetComponent<EntityAITurret>() != null)
        {
            GetComponent<EntityAITurret>().ResetCannon();
        }
    }

    public virtual void OnPlayerCollide(GameObject other)
    {
        if (scoreComponent != null && comboSystem != null)
        {
            switch (entityClass.entityType)
            {
                // If the entity is a collectible
                case EntityClass.EntityType.Collectible:
                    scoreComponent.AddScore(entityClass.score);
                    coinCount.AddCoin(entityClass.coinAmount);
                    break;
                // If the entity is a powerup, checks for the powerup script and call the function
                case EntityClass.EntityType.PowerUp:
                    scoreComponent.AddScore(entityClass.score);
                    GetComponent<GameSystemPowerUpComponent>()?.UsePowerUp(other);
                    break;
                // If the entity is a damageable
                case EntityClass.EntityType.Damageable:
                    scoreComponent.AddScore(entityClass.score);
                    break;
                // If the entity kills the player
                case EntityClass.EntityType.Deadly:
                    other.GetComponent<Player>()?.Death();
                    break;
                case EntityClass.EntityType.Ally:
                    // we'll not do anything to the player
                    return; 
                default:
                    break;
            }

            // When the player is still alive and active we start combo
            if (other.activeSelf == true)
            {
                comboSystem.StartCombo(); // Starts the combo or add to combo
            }
        }

        // Disables entity
        DestroyEntity();
    }

    public void DestroyEntity()
    {
        // Spawns particles based on entity color
        ParticleSystem particles = Instantiate(entityClass.particlesOnCollide, transform.position, Quaternion.identity);
        ParticleSystem.MainModule ma = particles.main;
        ma.startColor = entityClass.color;

        audioManager.Play(entityClass.audioOnCollide);

        if (gameObject.GetComponent<EntityProjectileRocket>() != null)
        {
            Destroy(gameObject);
            return;
        }

        gameObject.SetActive(false);
    }
}
