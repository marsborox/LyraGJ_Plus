using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
public enum Type { RED, GREEN, BLUE,WHITE }
public class Enemy : Unit
{
    //coefs point of view of enemy
    public float normalCoef = 1;
    public float advantageCoef = 0.5f;
    public float disadvantageCoef = 1.5f;

    public DirectionMovement greenGoingUp;
    public DirectionMovement greenGoingDown;
    public DirectionMovement greenGoingLeft;

    public DirectionMovement blueGoingUp;
    public DirectionMovement blueGoingDown;
    public DirectionMovement blueGoingLeft;

    public Type enemyType;
    public Unit player;
    public float range = 0.2f;
    bool isPlayerInRange = false;
    public int damage = 1;
    public float attackCooldown = 1f;
    float coolDownTimer;
    public float movementSpeed = 10f;

    public float knockBackThrust = 10f;
    bool isKnockedBack = false;
    public Rigidbody2D myRigidBody;
    public float knockBackTime = 0.2f;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetEnemyType(Type newEnemyType)
    {
        enemyType = newEnemyType;

        switch (enemyType)
        {
            case Type.RED:
                {
                    break;
                }
            case Type.GREEN:
                {
                    goingUp = greenGoingUp;
                    goingDown = greenGoingDown;
                    goingLeft = greenGoingLeft;
                    break;
                }
            case Type.BLUE:
                {
                    goingUp = blueGoingUp;
                    goingDown = blueGoingDown;
                    goingLeft = blueGoingLeft;
                    break;
                }
        }
    }

    // Update is called once per frame
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
        FaceCorrectDirection();
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
            /*
            if ((weapon.weaponType).ToString() == enemyType.ToString())
            {
                TakeDamage(weapon.damage);
                GetKnockBack();
            }
            else
            {
                GetKnockBack();
            }*/
            /*
            if ((other.GetComponent<Weapon>().weaponType.ToString() == enemyType.ToString()))
            {
                TakeDamage(other.GetComponent<Weapon>().damage);
                Debug.Log("GotHit");
                Destroy(gameObject);
            }
            else
            {
                GetKnockBack();
            }*/
        }
    }
    void AnalyseAndTakeDamage(Weapon inputWeapon)
    {
        int weaponTypeIndex = ConvertType(inputWeapon.weaponType);
        int enemyTypeIndex = ConvertType(enemyType);
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
        /*Debug.Log("Enemy type " + enemyType.ToString() + " took damage: " + damageTaken.ToString() 
            + " from weapon type " + inputWeapon.weaponType.ToString());*/
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
        bool isInRange = false;
        float distance = Vector3.Distance(player.transform.position,transform.position);
        float horizontalDistance = Mathf.Abs(player.transform.position.x - transform.position.x);
        float verticalDistance = Mathf.Abs(player.transform.position.y - transform.position.y);

        if (horizontalDistance >= verticalDistance)
        {
            if (player.transform.position.x < transform.position.x)
            {
                currentDirection = Direction.LEFT;
            }
            else if (player.transform.position.x >= transform.position.x)
            {
                currentDirection = Direction.RIGHT;
            }
        }
        else
        {
            if (player.transform.position.y > transform.position.y)
            {
                currentDirection = Direction.UP;
            }
            else if (player.transform.position.y <= transform.position.y)
            {
                currentDirection = Direction.DOWN;
            }
        }

        if (distance < range)
        {
            Attack();
        }
        else 
        {
            Move(player);

        }
        
    }
    public void Move(Unit target)
    {
        transform.position = Vector3.MoveTowards(transform.position,target.transform.position,movementSpeed*Time.deltaTime);
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
        {
            AttackHit();
        }
    }
    void AttackHit()
    {
        coolDownTimer = attackCooldown;
        player.TakeDamage(damage);
    }
    void GetKnockBack()
    {
        if (isKnockedBack)
            return;
        Vector2 difference = (transform.position - player.transform.position).normalized * knockBackThrust * myRigidBody.mass;
        myRigidBody.AddForce(difference,ForceMode2D.Impulse);
        isKnockedBack = true;
        StartCoroutine(KnockBackRoutine());
    }
    IEnumerator KnockBackRoutine()
    { 
        yield return new WaitForSeconds(knockBackTime);
        isKnockedBack = false;
        myRigidBody.linearVelocity = Vector3.zero;
    }
}
