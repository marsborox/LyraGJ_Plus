using System.Collections;
using UnityEngine;

public class Sword : Weapon
{
    public SimpleSpriteAnimator upAttackAnimator;
    public SimpleSpriteAnimator downAttackAnimator;
    public SimpleSpriteAnimator leftAttackAnimator;
    public Player player; // to get direction

    private SimpleSpriteAnimator currentAnimator;
    private Collider2D currentCollider;

    public override void AttackHit()
    {
        switch (player.currentDirection)
        {
            case Unit.Direction.UP:
                {
                    currentAnimator = upAttackAnimator;
                    break;
                }
            case Unit.Direction.DOWN:
                {
                    currentAnimator = downAttackAnimator;
                    break;
                }
            case Unit.Direction.LEFT:
                {
                    currentAnimator = leftAttackAnimator;
                    break;
                }
            case Unit.Direction.RIGHT:
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
