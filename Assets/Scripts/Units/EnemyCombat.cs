using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class EnemyCombat : UnitCombat
{
    public enum AttackPhase { READY, ANIMATION, POSTANIMATION,POSTHIT}
    public enum NotTriggeredPhase {MOVING, WAITING,DECIDING}
    public Player player;
    public EnemyBehavior_SO behaviorTemplate;
    public EnemyMovement enemyMovement;
    public Room roomISpawnedIn;
    //public Image healthBar; //working on
    public bool isShielded = true;
    [SerializeField] private Vector2 nonCombatDestination;

    [Tooltip("Set in %")]
    public float displayConversationChance = 5;

    [SerializeField] private GameObject _healthBarObject;
    private bool _isHit = false;

    [Tooltip("MUST BE IN %")]
    [SerializeField] private int _chanceForHealthPickup = 20;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private HealthPickup _healthPickup;
    [SerializeField] private EnemyConversation _conversation;

    [SerializeField] private EnemyShield _shield;
    [SerializeField]private AttackPhase _currentAttackPhase = AttackPhase.READY;
    [SerializeField]private NotTriggeredPhase _notTriggeredPhase = NotTriggeredPhase.DECIDING;
    public bool isTriggered;

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

    [Header("Damage")]
    [SerializeField] private DamageNumber damageNumberPrefab;
    [SerializeField] private Color damageNumberColorNoCrit = new Color32(252,112,2,255);
    [SerializeField] private Color damageNumberColorCrit = Color.green;
    [SerializeField] private EnemyDance enemyDancePrefab;
    [Header("Non Combat")]
    [SerializeField] float _minWaitTime = 0.2f;
    [SerializeField] float _maxWatiTime = 0.5f;
    [SerializeField] float _walkChance = 0.7f;
    void Start()
    {
        base.Start();
        enemyMovement.StopMovement();
    }
    private void OnEnable()
    {
        GlobalEventManager.OnPlayerAttack += ReactToPlayerAttack;
    }
    private void OnDisable()
    {
        GlobalEventManager.OnPlayerAttack -= ReactToPlayerAttack;
    }
    void Update()
    {
        if(!isTriggered) NotActivatedSwitch();
        
        //DecideNextBehavior();// we will remove this
        CooldownTimer();
        base.Update();
        if (healthCurrent <= 0)
        {
            // DieAnimation();
            Die();
        }
        
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
        
    }
    private void FixedUpdate()
    {
        //PerformEnemyBehavior();
        if(!isTriggered) return;

        if (isStunned)
            return;
        if (isPushedBack)
            return;
        AttackPhaseSwitch();
    }
        private void OnTriggerEnter2D(Collider2D other)
    {//toRemove
        //Debug.Log("GotHitBySomething");
        GetHitFromWeapon(other);
        GetHitFromExplosion(other);
        GetHitFromProjectile(other);
    }
    public void TriggerEnemy()
    {
        isTriggered = true;
    }
    public void StartAttackAnimation()
    {
        enemyMovement.StopMovement();
        //Debug.Log("Starting AttackAnimaiton");
        animationController.HandleEnemyAttackAnimation();
    
        //attackAnimationTimer = attackAnimationTime;
        isAttacking = true;
        _currentAttackPhase = AttackPhase.ANIMATION;
        //play attackAnimation
        
    }
    public override void PostAttackAnimationEventUnit()
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
    public override void TakeDamage(int damage, bool isCrit)
    {
        MySoundManager.instance.PlayLyraHit();

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
        //Debug.Log("taking damage from unitCombat");
        animationController.HandleTakeDamageAnimation();
        isStunned = true;
        stunDuration = 999;//stun is cancelled post get hit animation, timer is arbitrary
        
        //Debug.Log("Taking "+damage+" Damage");
        StartCoroutine(MakeDamageableAgainRoutine());

        ShowDamage(damage, isCrit);// this wil lchange color

        healthCurrent -= damage;
        //Debug.Log("Taking damage in enemyCombat");
        //Debug.Log(damage+" damage taken");
        ResetAttackAnimation();//this disables enemies attack on hit

    }
    public void DisableShield()
    {
        Destroy(_shield.gameObject);
        isShielded = false;
    }
    public override void Die()
    {
        //triggered by animationEvent
        //Debug.Log("Enemy died");
        if (GameManager.instance != null)
            GameManager.instance.EnemyDied();
        //Debug.Log("enemyDeath processing");
        CheckDropHealth();
        GlobalEventManager.instance.TriggerOnEnemyDied(_enemy, roomISpawnedIn);

        if (enemyDancePrefab != null)
        {
            EnemyDance enemyDance = Instantiate(enemyDancePrefab, transform.position, Quaternion.identity);
            enemyDance.StartDancing(_enemy.enemyType);            
        }

        Destroy(gameObject);

        //roomISpawnedIn.EnemyDied();
    }
    IEnumerator MakeDamageableAgainRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _isHit = false;
        //Debug.Log("Can be damaged again");
    }
    private void ReactToPlayerAttack()
    {
        TriggerEnemy();
        int randomRoll = Random.Range(0,100);
        //Debug.Log("Registering players attack with Random roll: " + randomRoll);
        
        if (randomRoll > displayConversationChance)
        { return; }
        _conversation.gameObject.SetActive(true);
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
    private void NotActivatedSwitch()
    {//Debug.Log("doing not activated ");
    
        switch(_notTriggeredPhase)
        {
            case NotTriggeredPhase.DECIDING:
                {
                    //Debug.Log("deciding");
                    // decide what to do next - move or wait
                    DecideNextBehavior();
                    return;
                }
            case NotTriggeredPhase.WAITING:
                {
                    //Debug.Log("waiting");
                    //jsut waiting, perhaps coroutine, when its done do deciding
                    return;
                }
            case NotTriggeredPhase.MOVING:
                {
                    //Debug.Log("moving");

                    if(Vector2.Distance(transform.position, nonCombatDestination)<2)//magic number
                        {
                        _notTriggeredPhase = NotTriggeredPhase.DECIDING;
                        enemyMovement.StopMovement();
                        }
                    animationController.HandleAnimationNoIdle();
                    return;
                }
            default: {return;}
        }
    }
    void DecideNextBehavior()
    {
        // some randomising
        //if moving run moveToTarget method in movement
        //if waiting, coroutine on its end run this again
  
        bool willMove = (float)Random.Range(0f,1f) <= _walkChance;
        //Debug.Log("willMove: "+willMove);
        if(willMove)
        {StartCoroutine(WaitingRoutine());}
        else
        {
            MoveRandomly();
        }
    }
    IEnumerator WaitingRoutine()
    {
        _notTriggeredPhase = NotTriggeredPhase.WAITING;
        float randomTime = (float)Random.Range(_minWaitTime,_maxWatiTime);
        yield return new WaitForSeconds(randomTime);
        _notTriggeredPhase = NotTriggeredPhase.DECIDING;
    }
    void MoveRandomly()
    {
        _notTriggeredPhase = NotTriggeredPhase.MOVING;
        Vector2 destination = GetRandomDestination();
        
        //Debug.Log("destination = " + destination.ToString());
        nonCombatDestination = destination;    
        enemyMovement.MoveToTarget(destination);

    }
    Vector2 GetRandomDestination()
    {
        bool isDestinaitonValid = false;

        Vector2 realDestination = new Vector2(0,0);
        
        while (isDestinaitonValid==false)

        {
            float xCoord;
            float yCoord;

            //get random position in room
            roomISpawnedIn.ReturnRandomPointRelative(out xCoord, out yCoord);
            Vector2 destination = new Vector2(xCoord,yCoord);

            destination += (Vector2)transform.position;
            NavMeshHit hit;
            realDestination = Vector2.zero;

            //Debug.Log("destination chosen randomly: "+destination);
            //get closest walkable point to random position
            if(NavMesh.SamplePosition(destination, out hit, Mathf.Infinity,1))
            {
                realDestination = hit.position;
            } 
            // check if the closest is in this room

                /*Debug.Log("destination = " + realDestination.ToString());
                nonCombatDestination = realDestination;    
                enemyMovement.MoveToTarget(realDestination);*/
                isDestinaitonValid = roomISpawnedIn.IsWithinBounds(realDestination);
                //Debug.Log("isDestinaionValid: "+isDestinaitonValid);
                if(!isDestinaitonValid)
                {
                    //Debug.LogError("destiantionNotValid Recalculating");
                }
        }    
        return realDestination;
    }

    private void AttackPhaseSwitch()
    {
        switch (_currentAttackPhase)
        {
            case AttackPhase.READY:
                {//SO behavior

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
                    PostAttackAnimationEventUnit();
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
    private void DieAnimation()
    {
        stunDuration = 999;
        isStunned = true;
        animationController.HandleDeathAnimation();
        enemyMovement.StopMovement();
        _healthBarObject.SetActive(false);
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
        private void ShowDamage(float amount, bool isCrit)

    {
        if (healthCurrent <= 0) return;
        
        Vector3 offset = new Vector3(0, 1.5f, 0); // to start just above enemy
        DamageNumber damageNumber = Instantiate(damageNumberPrefab, transform.position + offset, Quaternion.identity);
        damageNumber.Show(amount, isCrit ? damageNumberColorCrit : damageNumberColorNoCrit);
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

