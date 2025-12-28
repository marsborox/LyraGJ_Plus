using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public enum Type { RED, GREEN, BLUE, WHITE }
public class Enemy : Unit
{
    public Type enemyType;
    public SpriteLibrary spriteLibrary;
    public EnemyMovement enemyMovement;
    //public Enemy_SO enemyTemplate;

    
    [SerializeField] private UnitAnimationController _animationController;
    [SerializeField] private SpriteResolver _spriteResolver;
    [SerializeField] private GameObject _unitVisual;
    [SerializeField] private EnemyShield _enemShield;
    

    private void Awake()
    {
        //_animationController.animator.enabled = false;//broken
        //_unitVisual.SetActive(false);
    }
    private void Start()
    {
        base.Start();
    }
    public void SetProperties(Enemy_SO enemyTemplate, Player player)
    {
        var combat = (EnemyCombat)unitCombat;
        combat.range=enemyTemplate.range;
        combat.damage=enemyTemplate.damage;
        combat.attackCooldown=enemyTemplate.attackCooldown;
        enemyMovement.movementSpeed=enemyTemplate.movementSpeed;
        combat.healthMax = enemyTemplate.health;
        combat.player = player;
        combat.attackCooldown = enemyTemplate.attackCooldown;
        combat.attackAnimationTime = enemyTemplate.attackAnimationTime;
        spriteLibrary.spriteLibraryAsset = enemyTemplate.enemySpriteLibrary;
        //_spriteResolver.ResolveSpriteToSpriteRenderer();
        spriteLibrary.RefreshSpriteResolvers();//1
        _animationController.animator.Rebind();//2
        _animationController.animator.Update(0f);//3
        _animationController.animator.enabled = true;
        //_unitVisual.SetActive(true);
        //spriteLibrary.RefreshSpriteResolvers();
        SetEnemyType(enemyTemplate.enemyType);


        if (enemyTemplate.behavior != null)
        { 
            combat.behaviorTemplate = enemyTemplate.behavior;
        }
    }
    public void SetProperties(Enemy_SO enemyTemplate, Player player, Room room)
    {
        var combat = (EnemyCombat)unitCombat;
        combat.range = enemyTemplate.range;
        combat.damage = enemyTemplate.damage;
        combat.attackCooldown = enemyTemplate.attackCooldown;
        enemyMovement.movementSpeed = enemyTemplate.movementSpeed;
        combat.healthMax = enemyTemplate.health;
        combat.player = player;
        combat.attackCooldown = enemyTemplate.attackCooldown;
        combat.attackAnimationTime = enemyTemplate.attackAnimationTime;
        combat.roomISpawnedIn = room;
        spriteLibrary.spriteLibraryAsset = enemyTemplate.enemySpriteLibrary;
        //_spriteResolver.ResolveSpriteToSpriteRenderer();
        spriteLibrary.RefreshSpriteResolvers();//1
        _animationController.animator.Rebind();//2
        _animationController.animator.Update(0f);//3
        _animationController.animator.enabled = true;
        //_unitVisual.SetActive(true);
        //spriteLibrary.RefreshSpriteResolvers();
        SetEnemyType(enemyTemplate.enemyType);
        if (enemyTemplate.behavior != null)
        {
            combat.behaviorTemplate = enemyTemplate.behavior;
        }
    }
    public void SetEnemyType(Type newEnemyType)
    {
        enemyType = newEnemyType;
        Color32 color = new Color32();

        switch (enemyType)
        {
            case Type.RED:
                {
                    color = new Color32(255,0,0,40);
                    break;
                }
            case Type.GREEN:
                {
                    color = new Color32(0, 255, 0, 40);
                    break;
                }
            case Type.BLUE:
                {
                    color = new Color32(0, 0, 255, 40);
                    break;
                }
        }
        
        _enemShield.SetShieldType(enemyType, color);
    }
}
