using System.Collections;
using UnityEngine;
using DevZhrssh.Managers;

public class TurretAI : MonoBehaviour
{
    [SerializeField] Transform muzzleFlash;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float fireRate = 4f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] Transform indicator;
    private EntityClass entityClass;
    private Player target;
    private bool canFire;

    // Audio
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        target = FindObjectOfType<Player>();
        entityClass = GetComponent<Entity>()?.entityClass;

        canFire = true;
    }

    private void Update()
    {
        // Rotates toward the player
        HandleRotation();


        // Spawn projectile
        if (bulletPrefab != null)
        {
            if (canFire)
                // Fires Projectile
                StartCoroutine(Fire());
        }
        else
        {
            Debug.LogError("No Bullet Prefab!");
        }

    }

    private void HandleRotation()
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Fire()
    {
        if (entityClass.indicator == null) yield break; // Stops coroutine if the indicator is not set

        // Stops firing multiple times
        canFire = false;

        // Spawns indicator before firing
        GameObject obj = Instantiate(entityClass.indicator, indicator.position, Quaternion.identity);
        Destroy(obj, 1f); // Destroys indicator

        yield return new WaitForSeconds(1f);

        // Plays Audio
        if (audioManager != null)
            audioManager.Play("Shoot");

        // Fires projectile
        GameObject bullet = Instantiate(bulletPrefab, muzzleFlash.position, transform.rotation);
        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            // Sets the projectile target
            projectile.SetTarget(target.gameObject);
        }

        yield return new WaitForSeconds(fireRate);
        canFire = true; // Allows the turret to fire
    }
}
