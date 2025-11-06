using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public EnemyMovement enemyMovement;
    public Enemy_SO enemyTemplate;

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
    public void SetProperties(Enemy_SO enemyTemplate, Player player)
    { 
        ((EnemyCombat)unitCombat).range=enemyTemplate.range;
        ((EnemyCombat)unitCombat).damage=enemyTemplate.damage;
        ((EnemyCombat)unitCombat).attackCooldown=enemyTemplate.attackCooldown;
        enemyMovement.movementSpeed=enemyTemplate.movementSpeed;
        ((EnemyCombat)unitCombat).healthMax = enemyTemplate.health;
        ((EnemyCombat)unitCombat).player = player;
        SetEnemyType(enemyTemplate.enemyType);
        
    }
    
}
