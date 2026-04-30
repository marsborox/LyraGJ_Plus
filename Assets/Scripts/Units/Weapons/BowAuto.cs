using System.Collections;

using UnityEngine;

public class BowAuto : Weapon
{
    public Projectile projectilePrefab;
    private Coroutine _animationRoutine;
    [SerializeField] private Color _projectileColor = Color.green;
    [SerializeField] private Sprite projectileSprite;
    
    public override void ClickAttack()
    {
        ShootProjectile();
    }

    public void ShootProjectile()
    {
        if (_animationRoutine != null)
            return;
        if (!CanAttack())
        {
            return;
        }
        coolDownTimer = maxCooldown;
        Projectile projectile = Instantiate(projectilePrefab); //same w SO
        projectile.transform.position = player.transform.position;//same w SO
        projectile.transform.up = mouseFollow.transform.up;//similar w SO
        projectile.sourceUnit = player;//same w SO
        projectile.targetTag = player.targetTag;//same w SO
        projectile.gameObject.tag = player.unitCombat.projectileTagWeShot;//same w SO
        projectile.damage = ReturnCalculateDamage(out projectile.isCrit);//same w SO

        projectile.pushBackDuration = pushBackDuration;//not in SO
        projectile.pushBackForce = pushbackForce;//not in SO
        projectile.projectileType = weaponType;
        projectile.projectileSpriteRenderer.color = _projectileColor;
        projectile.projectileSpriteRenderer.sprite = projectileSprite;

        _animationRoutine = StartCoroutine(AnimationRoutine());
        GlobalEventManager.instance.TriggerOnPlayerAtack();
    }
    IEnumerator AnimationRoutine()
    { //simulates animation
        player.playerMovement.CanNotMove();
        yield return new WaitForSeconds(attackAnimationTime);
        player.playerMovement.CanMove();
        _animationRoutine = null;
    }/*
    //can remove
    public override void SetWeaponRed()
    {
        weaponType = Type.RED;
        _projectileColor = Color.red;
    }
    public override void SetWeaponGreen()
    {
        weaponType = Type.GREEN;
        _projectileColor = Color.green;
    }
    public override void SetWeaponBlue()
    {
        weaponType = Type.BLUE;
        _projectileColor = Color.blue;
    }*/
}
