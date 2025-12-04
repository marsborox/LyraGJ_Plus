using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public enum Type { RED, GREEN, BLUE, WHITE }
public class Enemy : Unit
{
    public Type enemyType;

    public DirectionMovement greenGoingUp;
    public DirectionMovement greenGoingDown;
    public DirectionMovement greenGoingLeft;

    public DirectionMovement blueGoingUp;
    public DirectionMovement blueGoingDown;
    public DirectionMovement blueGoingLeft;

    public SpriteLibrary spriteLibrary;

    public EnemyMovement enemyMovement;
    public Enemy_SO enemyTemplate;

    [SerializeField] private AnimationController _animationController;

    private void Awake()
    {
        _animationController.animator.enabled = false;//broken
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
        spriteLibrary.RefreshSpriteResolvers();
        _animationController.animator.Rebind();
        _animationController.animator.Update(0f);
        _animationController.animator.enabled = true;
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
                    enemyMovement.goingUp = greenGoingUp;
                    enemyMovement.goingDown = greenGoingDown;
                    enemyMovement.goingLeft = greenGoingLeft;
                    break;
                }
            case Type.BLUE:
                {
                    enemyMovement.goingUp = blueGoingUp;
                    enemyMovement.goingDown = blueGoingDown;
                    enemyMovement.goingLeft = blueGoingLeft;
                    break;
                }
        }
    }
}
