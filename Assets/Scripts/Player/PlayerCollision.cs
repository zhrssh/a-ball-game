using UnityEngine;
using DevZhrssh.Utilities;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;

public class PlayerCollision : MonoBehaviour
{
    // Camera Shake
    [Header("Camera Shake")]
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakeMagnitude = 0.4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle enemy function call
        if (collision.GetComponent<EntityObject>() as IDamageable != null)
        {
            // Play camera shake when hitting enemies
            if (cameraShake != null)
            {
                if (collision.GetComponent<EntityObject>().entityClass.entityType != EntityClass.EntityType.Collectible && gameObject.activeSelf) // If the player collides to a coin don't shake camera
                    StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
            }

            // Calls the enemy script OnPlayerCollide
            IDamageable damageable = collision.GetComponent<EntityObject>() as IDamageable;
            damageable.OnPlayerCollide(gameObject); // pass in the player game object
        } 
    }
}
