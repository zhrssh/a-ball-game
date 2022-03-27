using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.Utilities;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakeMagnitude = 0.4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle enemy function call
        if (collision.GetComponent<Enemy>() as IDamageable != null)
        {
            IDamageable damageable = collision.GetComponent<Enemy>() as IDamageable;
            damageable.OnPlayerCollide();

            // Play camera shake when hitting enemies
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude)); // Magic numbers
        }
    }
}
