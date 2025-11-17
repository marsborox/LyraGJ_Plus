using System.Collections;

using UnityEngine;

public class BowAuto : Weapon
{
    public Projectile projectilePrefab;
    private Coroutine _animationRoutine;
    
    public override void ClickAttack()
    {
        ShootProjectile();
    }
    public override void HoldAttack()
    {
        ShootProjectile();
    }

    public void ShootProjectile()
    {
        if (_animationRoutine != null)
            return;
        Projectile projectile = Instantiate(projectilePrefab); //same w SO
        projectile.transform.position = player.transform.position;//same w SO
        projectile.transform.up = mouseFollow.transform.up;//similar w SO
        projectile.sourceUnit = player;//same w SO
        projectile.targetTag = player.targetTag;//same w SO
        projectile.gameObject.tag = player.unitCombat.projectileTagWeShot;//same w SO
        projectile.damage = damage;//same w SO
        projectile.pushBackDuration = pushBackDuration;//not in SO
        projectile.pushBackForce = pushbackForce;//not in SO
        _animationRoutine = StartCoroutine(AnimationRoutine());
    }
    IEnumerator AnimationRoutine()
    { //simulates animation
        player.playerMovement.CanNotMove();
        yield return new WaitForSeconds(attackAnimationTime);
        player.playerMovement.CanMove();
        _animationRoutine = null;
    }
}
