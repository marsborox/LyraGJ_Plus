using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitCombat : MonoBehaviour
{
    public int healthMax = 10;
    public int healthCurrent;
    public float healthFraction;
    public Image healthBar;

    public string tagThatHitsUs;
    public string projectileTagThatHitsUs;
    public string projectileTagWeShot;

    public bool isPushedBack = false;
    public float pushBackTime = 0.2f;

    public bool isStunned = false;
    public float stunDuration;
    public float stunTimer;
    //public float pushBackForce = 10f;

    public Rigidbody2D myRigidBody;
    public Unit thisUnit;
    public UnitAnimationController animationController;
    public void Start()
    {
        healthCurrent = healthMax;
    }
    public void Update()
    {
        SetHealthBar();
        if (isStunned)
        {
            StunTimer();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("GotHitBySomething");
        GetHitFromWeapon(other);
        GetHitFromExplosion(other);
        GetHitFromProjectile(other);
    }
    public void GetHitFromWeapon(Collider2D other)
    {
        if (other.gameObject.tag == tagThatHitsUs)
        {
            //Debug.Log("collision w weapon");
            Weapon weapon = other.gameObject.GetComponent<WeaponCollider>().weaponIBelongTo;
            //AnalyseAndTakeDamage(weapon);
            weapon.DealHit(this.thisUnit);
            //Debug.Log("got hit by enemyWeapon");
            
        }
    }
    private void GetHitFromExplosion(Collider2D other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            //get damage
            Explosion explosion = other.gameObject.GetComponent<Explosion>();
            //Debug.Log("got hit by explosion");
        }
    }
    private void GetHitFromProjectile(Collider2D other)
    {
        if (other.gameObject.tag == projectileTagThatHitsUs)
        {
            //get damage
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            //Debug.Log("got hit by enemyProjectile");
            projectile.ProjectileHit(this.thisUnit);
            //get knocked back
            //get effect
        }
    }
    public virtual void SetHealthBar()
    {
        healthFraction = (float)healthCurrent / (float)healthMax;
        healthBar.fillAmount = healthFraction;
    }
    public virtual void TakeDamage(int damage)
    {
        animationController.HandleTakeDamageAnimation();
        /*Debug.Log("taking damage from unitCombat");
        isStunned = true;
        stunDuration = 999;//stun is cancelled post get hit animation, timer is arbitrary
        */
        healthCurrent -= damage;
    }
    public virtual void GetHeal(int heal)
    {
        healthCurrent += heal;
        if (healthCurrent > healthMax)
        {
            healthCurrent = healthMax;
        }
    }
    public void GetPushedBackFrom(Vector3 pushedFrom,float pushBackForce, float pushBackDuration)
    {
        if (isPushedBack)
            return;
        Vector2 difference = (transform.position - pushedFrom).normalized * pushBackForce * myRigidBody.mass;
        myRigidBody.AddForce(difference, ForceMode2D.Impulse);
        isPushedBack = true;
        StartCoroutine(PushBackRoutine(pushBackDuration));
    }
    public void GetPushedBackInDirection(float directionAngle, float pushBackForce, float pushBackDuration)
    {//this is broken and one daz will fix it
        //basically get me angle of object that hit me and use that angle
        if (isPushedBack)
            return;
        Debug.Log(directionAngle);
        //Debug.Log("beingPushedBack");
        float yDistance = 1f;
        float xDistance = 1/Mathf.Tan(directionAngle);

        Vector2 someVector = new Vector2(xDistance,yDistance);
        
        Vector3 direction = someVector.normalized * pushBackForce * myRigidBody.mass;
        //Debug.Log(direction);

        myRigidBody.AddForce(direction, ForceMode2D.Impulse);
        isPushedBack = true;
        StartCoroutine(PushBackRoutine(pushBackDuration));
}
    IEnumerator PushBackRoutine(float pushBackDuration)
    {
        yield return new WaitForSeconds(pushBackDuration);
        isPushedBack = false;
        myRigidBody.linearVelocity = Vector3.zero;
    }
    void StunTimer()
    {
        stunTimer -= Time.deltaTime;
        if (stunTimer < 0)
        { 
            isStunned = false;
        }
    }
    public void GetStunned(float time)
    {
        stunTimer = time;
        isStunned = true;
    }
    public void PostAttackAnimationEventUnit()
    {
        //Debug.Log("Post-AttackAnimation Event");
        if (this is EnemyCombat)
        {
            //Debug.Log("Post-AttackAnimation Event, on Enemy");
            ((EnemyCombat)this).AttackHitPostAnimation();
        }
    }
    public virtual void Die()
    { 
        
    }
}
