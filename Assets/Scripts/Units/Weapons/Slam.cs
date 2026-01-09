using UnityEngine;
using System.Collections;
public class Slam : Weapon
{
    [SerializeField] private Collider2D _myCollider;
    [SerializeField] private GameObject _animationVisual1;
    [SerializeField] private GameObject _animationVisual2;
    

    public override void ClickAttack()
    {
        if (!CanAttack())
            return;
        coolDownTimer = maxCooldown;
        //Debug.Log("player attacking");
        //Debug.Log("Sword. attackHit");
        //currentCollider = currentAnimator.gameObject.GetComponent<Collider2D>();

        //Quaternion rotation = Quaternion.Euler(0f, 0f, mouseFollow.transform.eulerAngles.z-90);
        //_visualCollider.transform.rotation = rotation;
        //_visualCollider.transform.rotation = mouseFollow.ReturnMouseDirection();
        _myCollider.enabled = true;
        _animationVisual1.SetActive(true);
        //_animationVisual2.SetActive(true);
        player.playerMovement.CanNotMove();
        StartCoroutine(AttackHitRoutine());
        StartCoroutine(AttackAnimationRoutine());
        StartCooldown();
        GlobalEventManager.instance.TriggerOnPlayerAtack();
    }
    IEnumerator AttackHitRoutine()
    {
        yield return new WaitForSeconds(0.02f);
        _myCollider.enabled = false;
        //Debug.Log("SlamCollider off");
    }
    IEnumerator AttackAnimationRoutine()
    {
        yield return new WaitForSeconds(attackAnimationTime);
        _animationVisual1.SetActive(false);
        //_animationVisual2.SetActive(false);
        player.playerMovement.CanMove();
    }
    public override void DealHit(Unit unit)
    {
        if (((EnemyCombat)unit.unitCombat).isShielded)
        {
            return;
        }
        unit.GetPushedBack(this.transform.position,pushbackForce,pushBackDuration);
        unit.TakeDamage(ReturnCalculateDamage());
    }
}
