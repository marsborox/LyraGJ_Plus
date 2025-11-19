using UnityEngine;
using System.Collections;
public class Sword : Weapon
{

    [SerializeField] private Collider2D _myCollider;
    [SerializeField] private GameObject _animationVisual;
    [SerializeField] private GameObject _visualCollider;
    

    public override void ClickAttack()
    {
        //Debug.Log("Sword. attackHit");
        //currentCollider = currentAnimator.gameObject.GetComponent<Collider2D>();

        //Quaternion rotation = Quaternion.Euler(0f, 0f, mouseFollow.transform.eulerAngles.z-90);
        //_visualCollider.transform.rotation = rotation;
        _visualCollider.transform.rotation = mouseFollow.ReturnMouseDirection();
        _myCollider.enabled = true;
        _animationVisual.SetActive(true);
        player.playerMovement.CanNotMove();
        StartCoroutine(AttackHitRoutine());
        StartCoroutine(AttackAnimationRoutine());

    }
    public override void DealHit(Unit unit)
    {
        unit.GetPushedBack(this.transform.position,pushbackForce,pushBackDuration);
        unit.TakeDamage(damage);
    }

    IEnumerator AttackHitRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        _myCollider.enabled = false;

    }
    IEnumerator AttackAnimationRoutine()
    {
        yield return new WaitForSeconds(attackAnimationTime);
        _animationVisual.SetActive(false);
        player.playerMovement.CanMove();
    }
}
