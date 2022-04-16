using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private bool chaseTarget;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float projectileSpeed;
    private GameObject target;

    private void Update()
    {
        HandleRotation();

        // Goes toward the target
        transform.position += transform.up * projectileSpeed * Time.deltaTime;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void HandleRotation()
    {
        if (this.target != null)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
