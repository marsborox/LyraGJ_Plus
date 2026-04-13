using UnityEngine;

[CreateAssetMenu(fileName = "MeleeBasic_SO", menuName = "Scriptable Objects/EnemyBehavior_SO/MeleeBasic_SO")]
public class MeleeBasic_SO : EnemyBehavior_SO
{
    public override void PerformBehavior(EnemyCombat source, Unit target)
    {
        
        if (!source.CheckIfInRange())
        {
            source.enemyMovement.MoveToTarget(source.player);

        }
        if (source.isAttackReady && source.CheckIfInRange())
        {
 
            source.enemyMovement.StayIdle();
            source.StartAttackAnimation();
        }
    }
    public override void PostAttackAction(EnemyCombat source, Unit target)
    {
        if (source.CheckIfInRange())
        {
            target.TakeDamage(source.damage);
        }

    }
}
