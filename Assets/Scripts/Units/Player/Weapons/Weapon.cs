using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public enum WeaponType {RED, GREEN, BLUE }

    public Type weaponType;
    public float maxCooldown = 1f;
    public int damage = 1;
    public MouseFollow mouseFollow;
    public float attackAnimationTime = 0.5f;

    public Player player;
    private float _coolDownTimer = 0;
    private void Start()
    {
        _coolDownTimer = 0;
    }

    public void Update()
    {
        CoolDownTimer();
    }
    public bool CanAttack()
    {
        return _coolDownTimer <= 0;
    } 
    public float CoolDownValue()
    {
        float currentTimer = _coolDownTimer < 0 ? 0 : _coolDownTimer;
        return maxCooldown - (currentTimer / maxCooldown);
    }

    public void CoolDownTimer()
    {
        if (_coolDownTimer > 0)
        { 
            _coolDownTimer-= Time.deltaTime;
        }
    }
    public void StartCooldown()
    {
        _coolDownTimer = maxCooldown;
    }
    public void Attack()
    {
        if (CanAttack())
        {
            _coolDownTimer = maxCooldown;
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
}