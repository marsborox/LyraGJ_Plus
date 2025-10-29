using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public enum WeaponType {RED, GREEN, BLUE }

    public Type weaponType;
    public float maxCooldown = 1f;
    public int damage = 1;

    private float coolDownTimer = 0;

    private void Start()
    {
        coolDownTimer = 0;
    }

    private void Update()
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
        return maxCooldown - (currentTimer / maxCooldown);
    }

    public void CoolDownTimer()
    {
        if (coolDownTimer > 0)
        { 
            coolDownTimer-= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (CanAttack())
        {
            coolDownTimer = maxCooldown;
            AttackHit();
        }
    }
    public virtual void AttackHit()
    {
        Debug.Log("AttackHit not implemented on this weapon");
    }
}
