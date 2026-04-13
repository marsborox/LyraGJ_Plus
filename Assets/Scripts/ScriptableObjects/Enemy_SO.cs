using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "Enemy_SO", menuName = "Scriptable Objects/Enemy_SO")]
public class Enemy_SO : ScriptableObject
{
    public Type enemyType;
    public SpriteLibraryAsset enemySpriteLibrary;

    public int damage = 1;
    public int health = 1;
    public float range = 0.2f;
    public float attackCooldown = 1f;
    public float movementSpeed = 1f;
    //public float attackSpeed = 100f;
    public float attackAnimationTime = 0.5f;
    

    public Color32 spriteColor;

    public EnemyBehavior_SO behavior;

    public bool forceShield;
    public bool randomShield;
    [Tooltip("Set this in percentage")]
    public int chanceForShield;
}
