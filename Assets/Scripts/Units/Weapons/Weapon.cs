using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public enum WeaponType {RED, GREEN, BLUE }

    public Type weaponType;
    public float maxCooldown = 1f;
    public float coolDownTimer = 0;
    public int damageBase = 1;
    public int damageApplied;
    public int damageMultiplier = 2;
    public float attackAnimationTime = 0.5f;
    public float pushbackForce;
    public float pushBackDuration;
    public string projectileTag = "PlayerProjectile";

    public MouseFollow mouseFollow;
    public Player player;

    

    private void Start()
    {
        coolDownTimer = 0;
    }

    public void Update()
    {
        CoolDownTimer();
    }
    public bool CanAttack()
    {
        return coolDownTimer <= 0;
    } 
    public float CoolDownValue()
    {
        float currentTimer = coolDownTimer < 0 ? 0 : coolDownTimer;
        float returnValue = 1 - (currentTimer / maxCooldown);//so it fills from empty to full
        
        //Debug.Log("weaponCdTimer " + currentTimer+ " valueFor imageFill "+ returnValue + " weapon "+this.name );
        return returnValue;
    }

    public void CoolDownTimer()
    {
        if (coolDownTimer > 0)
        { 
            coolDownTimer -= Time.deltaTime;
        }
    }
    public void StartCooldown()
    {
        coolDownTimer = maxCooldown;
    }
    public void Attack()
    {
        if (CanAttack())
        {
            coolDownTimer = maxCooldown;
            AttackHit();
        }
    }
    public virtual void ClickAttack()
    { 
        
    }
    public virtual void HoldAttack()
    {

    }
    public virtual void AttackHit()
    {  
        Debug.Log("AttackHit not implemented on this weapon");
    }
    public virtual void DealHit(Unit unit)
    { 
    
    }
    public void CalculateDamage()
    {
        if (player.rythmBonus.CheckIfInRythm())
        {
            damageApplied = damageBase * damageMultiplier;
        }
        else
        {
        damageApplied = damageBase;
        }
    }
    public int ReturnCalculateDamage()
    {
        int appliedDamage;
        if (player.rythmBonus.CheckIfInRythm())
        {
            appliedDamage = damageBase * damageMultiplier;
            //Debug.Log("doing crit damage: "+appliedDamage);
        }
        else
        {
            appliedDamage = damageBase;
            //Debug.Log("doing nonCrit damage: " + appliedDamage);
        }
        return appliedDamage;
    }
}