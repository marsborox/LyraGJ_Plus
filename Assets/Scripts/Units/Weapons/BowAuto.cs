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
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.transform.position = player.transform.position;
        projectile.transform.up = mouseFollow.transform.up;
        projectile.sourceUnit = player;
        projectile.targetTag = player.targetTag;

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
