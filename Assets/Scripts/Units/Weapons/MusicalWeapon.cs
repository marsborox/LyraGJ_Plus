using System.Collections;

using UnityEditor.Animations;

using UnityEngine;

public class MusicalWeapon : Weapon
{
    public SimpleSpriteAnimator upAttackAnimator;
    public SimpleSpriteAnimator downAttackAnimator;
    public SimpleSpriteAnimator leftAttackAnimator;
    
    public PlayerMovement playerMovement;

    private SimpleSpriteAnimator currentAnimator;
    private Collider2D currentCollider;
    [SerializeField] private MouseFollow _mouseFollow;

    private void Update()
    {
        base.Update();
        FollowMouse();
    }
    public override void AttackHit()
    {
        switch (playerMovement.currentDirection)
        {
            case PlayerMovement.Direction.UP:
                {
                    currentAnimator = upAttackAnimator;
                    break;
                }
            case PlayerMovement.Direction.DOWN:
                {
                    currentAnimator = downAttackAnimator;
                    break;
                }
            case PlayerMovement.Direction.LEFT:
                {
                    currentAnimator = leftAttackAnimator;
                    break;
                }
            case PlayerMovement.Direction.RIGHT:
                {
                    currentAnimator = leftAttackAnimator;
                    break;
                }
        }
        currentCollider = currentAnimator.gameObject.GetComponent<Collider2D>();
        currentCollider.enabled = true;
        StartCoroutine(AttackHitRoutine());
    }
    IEnumerator AttackHitRoutine()
    {
        currentAnimator.Play();
        yield return new WaitForSeconds(0.3f);
        currentCollider.enabled = false;
    }
    private void FollowMouse()
    { 
        transform.rotation = _mouseFollow.transform.rotation;
    }
}
