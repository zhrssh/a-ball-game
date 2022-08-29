using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private bool chaseTarget;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float projectileSpeed;
    private GameObject target;

    private Entity entity;
    private EntityClass entityClass;

    private void Start()
    {
        entity = GetComponent<Entity>();
        entityClass = entity.entityClass;
    }

    private void Update()
    {
        HandleRotation();

        // Checks if target is still active, we destroy the rocket
        if (target.gameObject.activeSelf == false)
            entity.DestroyEntity();

        // Adds forward movement
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the rocket collides with its target
        Entity otherEntity = collision.GetComponent<Entity>();
        if (target != null && otherEntity != null)
        {
            Entity targetEntity = target.GetComponent<Entity>();
            if (targetEntity != null)
            {
                // If the collision and target matches, we destroy both
                if (otherEntity == targetEntity)
                {
                    otherEntity.DestroyEntity();
                    entity.DestroyEntity();
                }
                else
                {
                    entity.DestroyEntity();
                }
            }
        }
    }
}
