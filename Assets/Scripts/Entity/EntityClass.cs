using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity/New Entity")]
public class EntityClass : ScriptableObject
{
    public enum EntityType { Collectible, PowerUp, Damageable, Deadly }
    public EntityType entityType;
    public Sprite sprite;
    public Color color;
    public ParticleSystem particlesOnCollide;
    public string audioOnCollide;
    public int score;
    public int coinAmount;
    public GameObject indicator;
}
