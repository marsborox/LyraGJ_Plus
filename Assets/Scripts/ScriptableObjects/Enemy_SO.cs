using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_SO", menuName = "Scriptable Objects/Enemy_SO")]
public class Enemy_SO : ScriptableObject
{
    public Type enemyType;
    public float range = 0.2f;
    public int damage = 1;
    public float attackCooldown = 1f;
    public float movementSpeed = 1f;
    public float attackSpeed = 100f;
    public float attackAnimationTime = 0.5f;
    public int health = 1;
    public Color32 spriteColor;

    public EnemyBehavior_SO behavior;

}
