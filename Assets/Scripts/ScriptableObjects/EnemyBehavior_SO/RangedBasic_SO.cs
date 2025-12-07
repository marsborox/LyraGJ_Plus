using UnityEngine;

[CreateAssetMenu(fileName = "RangedBasic_SO", menuName = "Scriptable Objects/EnemyBehavior_SO/RangedBasic_SO")]
public class RangedBasic_SO : EnemyBehavior_SO
{
    public Projectile projectilePrefab;
    public override void PerformBehavior(EnemyCombat source, Unit target)
    {
        if (!source.isAttackReady)
        {
            source.CooldownTimer();
        }

        if (!source.CheckIfInRange())
        {

            source.enemyMovement.MoveToTarget(source.player);
            //source.enemyMovement.animationController.HandleAnimation();
            source.enemyMovement.animationController.HandleAnimationNoIdle();
        }
        if (source.isAttackReady && source.CheckIfInRange())
        {
            source.StartAttackAnimation();
            //ShootProjectile(source, target);
        }
    }
    public override void PostAttackAction(EnemyCombat source, Unit target) 
    {
        Projectile projectile = Instantiate(projectilePrefab); //same w weapon
        projectile.transform.position = source.transform.position; //same w weapon

        Vector3 direction = target.transform.position - source.transform.position;

        projectile.transform.up = direction.normalized; //similar w weapon
        projectile.sourceUnit = source.thisUnit;//same w weapon
        projectile.targetTag = source.thisUnit.targetTag;//same w weapon
        projectile.gameObject.tag = source.projectileTagWeShot;//same w weapon
        projectile.damage = source.damage;//same w weapon
        //projectile.pushBackDuration = pushBackDuration;//missing intentionally
        //projectile.pushBackForce = pushbackForce;//missing intentionally
    }

    public void ShootProjectile(EnemyCombat source, Unit target)
    {
        Projectile projectile = Instantiate(projectilePrefab); //same w weapon
        projectile.transform.position = source.transform.position; //same w weapon

        Vector3 direction = target.transform.position - source.transform.position;

        projectile.transform.up = direction.normalized; //similar w weapon
        projectile.sourceUnit = source.thisUnit;//same w weapon
        projectile.targetTag = source.thisUnit.targetTag;//same w weapon
        projectile.gameObject.tag = source.projectileTagWeShot;//same w weapon
        projectile.damage = source.damage;//same w weapon
        //projectile.pushBackDuration = pushBackDuration;//missing intentionally
        //projectile.pushBackForce = pushbackForce;//missing intentionally
    }
}
