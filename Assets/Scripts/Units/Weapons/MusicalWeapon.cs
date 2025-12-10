using System.Collections;
using UnityEngine;

public class MusicalWeapon : Weapon
{
    public SimpleSpriteAnimator upAttackAnimator;
    public SimpleSpriteAnimator downAttackAnimator;
    public SimpleSpriteAnimator leftAttackAnimator;
    
    public PlayerMovement playerMovement;

    private SimpleSpriteAnimator _currentAnimator;
    private Collider2D _currentCollider;
    [SerializeField] private MouseFollow _mouseFollow;

    private void Update()
    {
        base.Update();
        //FollowMouse();
    }
    public override void AttackHit()
    {
        switch (playerMovement.currentDirection)
        {
            case PlayerMovement.Direction.UP:
                {
                    _currentAnimator = upAttackAnimator;
                    break;
                }
            case PlayerMovement.Direction.DOWN:
                {
                    _currentAnimator = downAttackAnimator;
                    break;
                }
            case PlayerMovement.Direction.LEFT:
                {
                    _currentAnimator = leftAttackAnimator;
                    break;
                }
            case PlayerMovement.Direction.RIGHT:
                {
                    _currentAnimator = leftAttackAnimator;
                    break;
                }
        }
        _currentCollider = _currentAnimator.gameObject.GetComponent<Collider2D>();
        _currentCollider.enabled = true;
        StartCoroutine(AttackHitRoutine());
    }
    IEnumerator AttackHitRoutine()
    {
        _currentAnimator.Play();
        yield return new WaitForSeconds(0.3f);
        _currentCollider.enabled = false;
    }
    private void FollowMouse()
    { 
        transform.rotation = _mouseFollow.transform.rotation;
    }
}
