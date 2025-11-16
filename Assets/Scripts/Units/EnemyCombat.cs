using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCombat : UnitCombat
{
    public enum AttackPhase { READY, ANIMATION, POSTANIMATION,POSTHIT}
    public Player player;

    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private Enemy _enemy;

    [Header("combatStats")]

    public float range = 0.2f;
    //bool isPlayerInRange = false;

    public int damage = 1;
    public float attackCooldown = 1f;
    public float coolDownTimer = 0f;
    private bool isAttackReady = true;
    //public float movementSpeed = 1f;

    public float attackAnimationTime = 0.5f;
    public float attackAnimationTimer = 0f;
    public bool isAttacking = false;

    public bool canMove = true;
    
    //coefs point of view of enemy

    [Header(" type coef")]
    public float normalCoef = 1;
    public float advantageCoef = 0.5f;
    public float disadvantageCoef = 1.5f;

    [SerializeField]private AttackPhase _currentAttackPhase = AttackPhase.READY;

    void Update()
    {

        base.Update();
        if (isStunned)
        {

            return;
        }
        if (isPushedBack)
        {
            return;
        }
        if (!isPushedBack)
        {
            myRigidBody.linearVelocity = Vector3.zero;
        }
        //PerformTimers();
        
        //checkDirection
        
        if (healthCurrent <= 0)
        {
            Die();
        }
    }
    private void FixedUpdate()
    {
        //PerformEnemyBehavior();
        if (isStunned)
            return;
        BehaviorSwitch();
    }


    void AnalyseAndTakeDamage(Weapon inputWeapon)
    {
        int weaponTypeIndex = ConvertType(inputWeapon.weaponType);
        int enemyTypeIndex = ConvertType(_enemy.enemyType);
        int damageTaken;

        if (weaponTypeIndex == 9999)
        {//if is no special type
            damageTaken = inputWeapon.damage;
        }
        else
        //bigger brackets are attacking weapon, inside are coef values of
        //enemy type receiving dmg from that wpn
        {
            float[,] matrix = { { normalCoef, disadvantageCoef, advantageCoef },
                            { advantageCoef, normalCoef, disadvantageCoef },
                            { disadvantageCoef, advantageCoef, normalCoef } };
            float usedModifier = matrix[weaponTypeIndex, enemyTypeIndex];
            damageTaken = (int)(inputWeapon.damage * usedModifier);
        }

        TakeDamage(damageTaken);
        //Debug.Log("Enemy type " + _enemy.enemyType.ToString() + " took damage: " + damageTaken.ToString() 
        //    + " from weapon type " + inputWeapon.weaponType.ToString());
        //GetPushedBack();
    }
    int ConvertType(Type inputType)
    {
        switch (inputType)
        {
            case Type.RED:
                {
                    return 1;
                }
            case Type.GREEN:
                {
                    return 0;
                }
            case Type.BLUE:
                {
                    return 2;
                }
            default: return 9999;
        }
    }
    void Die()
    {
        //Debug.Log("MotherFucker died");
        GameManager.instance.EnemyDied();
        Destroy(gameObject);
    }

    void BehaviorSwitch()
    {
        switch (_currentAttackPhase)
        { 
            case AttackPhase.READY:
                {
                    if (CheckIfInRange())
                    {
                        //start attack animation
                        StartAttackAnimation();
                    }
                    else
                    {
                        _enemyMovement.MoveToTarget(player);
                    }
                    return;
                }
            case AttackPhase.ANIMATION:
                {
                    AttackAnimationTimer();
                    return;
                }
            case AttackPhase.POSTANIMATION:
                {
                    AttackHitPostAnimation();
                    return;
                }
            case AttackPhase.POSTHIT:
                {// ***************** remove this
                    if (!CheckIfInRange())
                    {
                        _enemyMovement.MoveToTarget(player);
                    }
                    CooldownTimer();
                    return;
                }
            default :
                {
                    Debug.Log("AttackPhase not implemented");
                return;
                }
        }
    }
    bool CheckIfInRange()
    {
        //bool isInRrange;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return (distance < range);

    }
    void StartAttackAnimation()
    {
        //Debug.Log("Starting AttackAnimaiton");
        //coolDownTimer = attackCooldown; // move to post hit prob
        attackAnimationTimer = attackAnimationTime;
        isAttacking = true;
        _currentAttackPhase = AttackPhase.ANIMATION;
        //isAttackReady= false;//move to post hit or not we want to make sure it wont attack many times
        //play attackAnimation
    }

    void AttackAnimationTimer()
    {
        if (!(attackAnimationTimer < 0))
        {
            attackAnimationTimer -= Time.deltaTime;
            if (attackAnimationTimer < 0)
            {
                isAttacking = false;
                AttackHitPostAnimation();
            }
        }
    }
    void AttackHitPostAnimation()
    {
        //Debug.Log("AttackAnimation ended, switching to posthitCooldown;");
        //this must exist bcs when well have normal animation we will use this tere
        if (CheckIfInRange())
        {
            player.TakeDamage(damage); //does not do anything rn
        }
        
        coolDownTimer = attackCooldown;
        isAttackReady = false;
        _currentAttackPhase = AttackPhase.POSTHIT;
    }
    public void CooldownTimer()
    {
        if (!(coolDownTimer < 0))
        {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer < 0) 
            { 
                isAttackReady = true;
                _currentAttackPhase = AttackPhase.READY;
                //Debug.Log("Attack Ready");
            }
        }
    }

}
/*
void PerformTimers()
{
    CooldownTimer();
    //PerformTimer(ref attackAnimationTimer, ref isAttacking);
    AttackAnimationTimer();

}*/

/*
public void PerformTimer(ref float timer, ref bool indicator)
{
    if (!(timer < 0))
    {
        timer -= Time.deltaTime;
        if (timer < 0) { indicator = true; }
    }
}
*/

/*private void StartCooldown()
{
    coolDownTimer = attackCooldown; // move to post hit prob
    isAttackReady = false;//move to post hit or not we want to make sure it wont attack many times
}*/
#region PushBack
/*
void GetPushedBack()
{
    if (isPushedBack)
        return;
    Vector2 difference = (transform.position - player.transform.position).normalized * pushBackForce * myRigidBody.mass;
    myRigidBody.AddForce(difference, ForceMode2D.Impulse);
    isPushedBack = true;
    StartCoroutine(PushBackRoutine());
}
IEnumerator PushBackRoutine()
{
    yield return new WaitForSeconds(pushBackTime);
    isPushedBack = false;
    myRigidBody.linearVelocity = Vector3.zero;
}*/
#endregion

