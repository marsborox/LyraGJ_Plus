using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;


public class EnemyCombat : UnitCombat
{

    public Player player;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private Enemy _enemy;

    [Header("combatStats")]

    public float range = 0.2f;
    //bool isPlayerInRange = false;

    public int damage = 1;
    public float attackCooldown = 1f;
    float coolDownTimer;
    public float movementSpeed = 10f;
    public float attackSpeed = 100f;
    private float _attackInterval;//interval is speed/100 in seconds
    private float _attackTimer;
    public float attackAnimationTime;
    [SerializeField] private float _attackAnimationProgress;
    private bool _isAttacking=false;

    public float knockBackThrust = 10f;
    bool isKnockedBack = false;
    public Rigidbody2D myRigidBody;
    public float knockBackTime = 0.2f;

    //coefs point of view of enemy

    [Header(" type coef")]
    public float normalCoef = 1;
    public float advantageCoef = 0.5f;
    public float disadvantageCoef = 1.5f;

    
    void Update()
    {
        base.Update();
        if (isKnockedBack)
        {
            return;
        }
        if (!isKnockedBack)
        {
            myRigidBody.linearVelocity = Vector3.zero;
        }
        PerformEnemyBehavior();
        //checkDirection

        if (healthCurrent <= 0)
        {
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collision");
        if (other.gameObject.tag == "PlayerWeapon")
        {
            //Debug.Log("collision w weapon");
            Weapon weapon = other.gameObject.GetComponent<WeaponCollider>().weaponIBelongTo;
            AnalyseAndTakeDamage(weapon);

        }
    }
    void AnalyseAndTakeDamage(Weapon inputWeapon)
    {
        int weaponTypeIndex = ConvertType(inputWeapon.weaponType);
        int enemyTypeIndex = ConvertType(_enemy.enemyType);
        //bigger brackets are attacking weapon, inside are coef values of
        //enemy type receiving dmg from that wpn
        int damageTaken;

        if (weaponTypeIndex == 9999)
        {//if is no special type
            damageTaken = inputWeapon.damage;
        }
        else
        {
            float[,] matrix = { { normalCoef, disadvantageCoef, advantageCoef },
                            { advantageCoef, normalCoef, disadvantageCoef },
                            { disadvantageCoef, advantageCoef, normalCoef } };
            float usedModifier = matrix[weaponTypeIndex, enemyTypeIndex];
            damageTaken = (int)(inputWeapon.damage * usedModifier);
        }

        TakeDamage(damageTaken);
        Debug.Log("Enemy type " + _enemy.enemyType.ToString() + " took damage: " + damageTaken.ToString() 
            + " from weapon type " + inputWeapon.weaponType.ToString());
        GetKnockBack();
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
    void PerformEnemyBehavior()
    {
        if (player == null)
        {
            return;
        }
        if (CheckIfInRange())
        {
            Attack();
        }
        else
        {
        _enemyMovement.MoveToTarget(player);
        }
    }
    public void CoolDownTimer()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (coolDownTimer < 0)
        {//add here attack animation

            AttackHit();
        }
    }
    void AttackHit()
    {
        //play attackAnimation

        coolDownTimer = attackCooldown;

        player.TakeDamage(damage); //does not do anything rn
    }
    void StartAttackAnimation()
    { 
        

    }
    void GetKnockBack()
    {
        if (isKnockedBack)
            return;
        Vector2 difference = (transform.position - player.transform.position).normalized * knockBackThrust * myRigidBody.mass;
        myRigidBody.AddForce(difference, ForceMode2D.Impulse);
        isKnockedBack = true;
        StartCoroutine(KnockBackRoutine());
    }
    IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        isKnockedBack = false;
        myRigidBody.linearVelocity = Vector3.zero;
    }
    bool CheckIfInRange()
    {
        bool isInRrange;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < range)
        {
            isInRrange = true;
        }
        else
        {
            isInRrange = false;
        }
        //isInRrange = distance < range;
        return isInRrange;
    }
}
