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

    [SerializeField] private AnimationController _animationController;
    [SerializeField] private SpriteResolver _spriteResolver;
    [SerializeField] private GameObject _unitVisual;

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
    public void SetEnemyType(Type newEnemyType)
    {
        enemyType = newEnemyType;

        switch (enemyType)
        {
            case Type.RED:
                {
                    break;
                }
            case Type.GREEN:
                {

                    break;
                }
            case Type.BLUE:
                {

                    break;
                }
        }
    }
}
