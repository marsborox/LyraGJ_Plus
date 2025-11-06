using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public UnitStats unitStats;



    
    [SerializeField] private Image _healthBar;

    public int healthMax = 10;
    public int healthCurrent;

    public float healthFraction;

    public string targetTag;
    
    void Start()
    {
        healthCurrent = healthMax;
    }
    
    public void Update()
    {
        SetHealthBar();
    }
    private void OnEnable()
    { 

    }
        
    void SetHealthBar()
    {
        healthFraction = (float)healthCurrent / (float)healthMax;
        _healthBar.fillAmount = healthFraction;
    }
    public void TakeDamage(int damage)
    {
        healthCurrent -= damage;
        //Debug.Log(damage+" damage taken");
    }
    public virtual void Die()
    { }
    public float ReturnHealthCurrent()
    {
        //return unitHealth.healthCurrent;
        return healthCurrent;
    }
    public float ReturnHealthMax()
    {
        return unitStats.healthMax_s.amount;
    }
    public float ReturnDamageAmount()
    {
        return unitStats.damage_s.amount;
    }
    public float ReturnMovementSpeedAmount()
    {
        return unitStats.movementSpeed_s.amount;
    }
    public float ReturnAttackSpeedAmount()
    {
        return unitStats.attackSpeed_s.amount;
    }
    public float ReturnAttackIntervalAmount()
    {
        return unitStats.attackInterval;
    }
    public float ReturnAttackTimer()
    {
        return unitStats.attackTimer;
    }
    /*public float ReturnScoreAmount()
    {
        return unitStats.score;
    }*/
    public void TakeDamage(float damageAmount)
    {
        //unitEventHandler.ChangeHealth(-damageAmount);
    }
    public void GetHeal(float healAmount)
    {
        //unitEventHandler.ChangeHealth(healAmount);
    }
    public void Attack()
    {
        //unitEventHandler.Attack();
    }
    public void AddScore(int score)
    {
        //unitStats.score += score;
    }

}
