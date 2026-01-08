using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;


public class EnemyCombat : UnitCombat
{
    public enum AttackPhase { READY, ANIMATION, POSTANIMATION,POSTHIT}
    public Player player;
    public EnemyBehavior_SO behaviorTemplate;
    public EnemyMovement enemyMovement;
    public bool isShielded = true;
    private bool _isHit = false;

    [Tooltip("MUST BE IN %")]
    [SerializeField] private int _chanceForHealthPickup = 20;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private HealthPickup _healthPickup;

    public Room roomISpawnedIn;

    [SerializeField] private EnemyShield _shield;
    [Header("combatStats")]

    public float range = 0.2f;
    //bool isPlayerInRange = false;

    public int damage = 1;
    public float attackCooldown = 1f;
    public float coolDownTimer = 0f;
    public bool isAttackReady = true;
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
        CooldownTimer();
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
        if (isPushedBack)
            return;
        BehaviorSwitch();
    }
    private void BehaviorSwitch()
    {
        switch (_currentAttackPhase)
        {
            case AttackPhase.READY:
                {//SO behav

                    //BehaviorTest();
                    behaviorTemplate.PerformBehavior(this, player);//triggers startAttackAnimation
                    return;
                }
            case AttackPhase.ANIMATION:
                {//this will be gone and handled on animator
                    //AttackAnimationTimer();
                    return;
                }
            case AttackPhase.POSTANIMATION:
                {//this will be gone and initiated on animator
                    AttackHitPostAnimation();
                    return;
                }
            case AttackPhase.POSTHIT:
                {
                    return;
                }
            default:
                {
                    Debug.Log("AttackPhase not implemented");
                    return;
                }
        }
    }
    public void StartAttackAnimation()
    {
        //Debug.Log("Starting AttackAnimaiton");
        animationController.HandleEnemyAttackAnimation();
        //attackAnimationTimer = attackAnimationTime;
        isAttacking = true;
        _currentAttackPhase = AttackPhase.ANIMATION;
        //play attackAnimation
    }

    public void AttackHitPostAnimation()
    {
        isAttacking = false;
        //Debug.Log("AttackAnimation ended, switching to posthitCooldown;");
        //this must exist bcs when well have normal animation we will use this tere
        //will be initiated by animation event
        behaviorTemplate.PostAttackAction(this,player);
        coolDownTimer = attackCooldown;
        isAttackReady = false;
        _currentAttackPhase = AttackPhase.READY;
    }

    public bool CheckIfInRange()
    {
        //bool isInRrange;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return (distance < range);
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
    public override void TakeDamage(int damage)
    {
        if (isShielded)
        {
            //Debug.Log("is shielded");
            _isHit = true;
            StartCoroutine(MakeDamageableAgainRoutine());
            //Debug.Log("make damagable shield routine");
            return;
        }
        if (_isHit)
        {
            //Debug.Log("Can not take"+damage+" damage");
            return;
        }
        _isHit = true;
        //Debug.Log("Taking "+damage+" Damage");
        StartCoroutine(MakeDamageableAgainRoutine());
        healthCurrent -= damage;
        //Debug.Log("Taking damage in enemyCombat");
        //Debug.Log(damage+" damage taken");
        ResetAttackAnimation();
    }
    public void DisableShield()
    {
        Destroy(_shield.gameObject);
        isShielded = false;
    }
    IEnumerator MakeDamageableAgainRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _isHit = false;
        //Debug.Log("Can be damaged again");
    }
    private void Die()
    {
        //Debug.Log("Enemy died");
        if(GameManager.instance !=null) 
        GameManager.instance.EnemyDied();
        //Debug.Log("enemyDeath processing");
        CheckDropHealth();
        GlobalEventManager.instance.TriggerOnEnemyDied(_enemy, roomISpawnedIn);
        Destroy(gameObject);

        //roomISpawnedIn.EnemyDied();
    }
    private void CheckDropHealth()
    {
        int drop = Random.Range(0, 100);
        if (drop < _chanceForHealthPickup)
        {
            HealthPickup healthPickup = Instantiate(_healthPickup);
            healthPickup.transform.position = transform.position;
        }
    }
    private void BehaviorTest()//for simplifying behavior switch
    {
        if (!CheckIfInRange())
        {
            enemyMovement.MoveToTarget(player);
            return;
        }
        if (isAttackReady)
        {
            StartAttackAnimation();
        }
        else 
        {
            CooldownTimer();
        }
    }

    

    public void ResetAttackAnimation()
    {
        if (_currentAttackPhase == AttackPhase.ANIMATION)
        {
            isAttacking = false;
            attackAnimationTimer = -0.0001f;//basically set to zero
            _currentAttackPhase = AttackPhase.READY;
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
#region old type conversion
/*void AnalyseAndTakeDamage(Weapon inputWeapon)
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
}*/
#endregion

