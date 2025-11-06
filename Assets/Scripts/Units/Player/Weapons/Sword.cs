using System.Collections;
using UnityEngine;

public class Sword : Weapon
{
    public SimpleSpriteAnimator upAttackAnimator;
    public SimpleSpriteAnimator downAttackAnimator;
    public SimpleSpriteAnimator leftAttackAnimator;
    public Player player; // to get direction
    public PlayerMovement playerMovement;

    private SimpleSpriteAnimator currentAnimator;
    private Collider2D currentCollider;

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
}
