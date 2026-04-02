using UnityEngine;
using System.Collections;
public class Sword : Weapon
{

    [SerializeField] private Collider2D _myCollider;
    [SerializeField] private GameObject _animationVisual;
    [SerializeField] private GameObject _visualCollider;
    [SerializeField] private SpriteRenderer _swordAttackSprite;

    public override void ClickAttack()
    {
        //Debug.Log("Sword. attackHit");
        //currentCollider = currentAnimator.gameObject.GetComponent<Collider2D>();
        //Quaternion rotation = Quaternion.Euler(0f, 0f, mouseFollow.transform.eulerAngles.z-90);
        //_visualCollider.transform.rotation = rotation;

        if (!CanAttack())
            return;
        coolDownTimer = maxCooldown;
        //Debug.Log("player attacking");
        _visualCollider.transform.rotation = mouseFollow.ReturnMouseDirection();
        _myCollider.enabled = true;
        // _animationVisual.SetActive(true); HIDE COLLIDER ANIMATION FOR NOW
        player.playerMovement.CanNotMove();
        StartCoroutine(AttackHitRoutine());
        StartCoroutine(AttackAnimationRoutine());
        //Attack();
        GlobalEventManager.instance.TriggerOnPlayerAtack();
    }
    public override void DealHit(Unit unit)
    {
        if (((EnemyCombat)unit.unitCombat).isShielded)
        {
            return;
        }
        unit.GetPushedBack(this.transform.position,pushbackForce,pushBackDuration);
        bool isCrit;
        unit.TakeDamage(ReturnCalculateDamage(out isCrit),isCrit);
    }
    public override void SetWeaponRed()
    {
        weaponType = Type.RED;
        _swordAttackSprite.color = Color.red;
    }
    public override void SetWeaponGreen()
    {
        weaponType = Type.GREEN;
        _swordAttackSprite.color = Color.green;
    }
    public override void SetWeaponBlue() 
    {
        weaponType = Type.BLUE;
        _swordAttackSprite.color = Color.blue;
    }

    IEnumerator AttackHitRoutine()
    {
        yield return new WaitForSeconds(0.02f);
        _myCollider.enabled = false;
        //Debug.Log("sword collider off");
    }
    IEnumerator AttackAnimationRoutine()
    {
        yield return new WaitForSeconds(attackAnimationTime);
        _animationVisual.SetActive(false);
        player.playerMovement.CanMove();
    }
}
