using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public UnitStats unitStats;
    public UnitCombat unitCombat;
    public string targetTag;
    
    public void Update()
    {
        
    }

    private void OnEnable()
    { 

    }
        
    public virtual void Die()
    { }
    public float ReturnHealthCurrent()
    {
        //return unitHealth.healthCurrent;
        return unitCombat.healthCurrent;
    }
    public float ReturnHealthMax()
    {
        return unitStats.healthMax_s.amount;
    }
    public int ReturnDamageAmount()
    {
        return (int)unitStats.damage_s.amount;
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
    public void SetMaxHealth(int health)
    {
        unitCombat.healthMax = health;
    }
    /*public float ReturnScoreAmount()
    {
        return unitStats.score;
    }*/
    public void TakeDamage(int damageAmount)
    {
        unitCombat.TakeDamage(damageAmount);
    }
    public void GetHeal(float healAmount)
    {
        //unitEventHandler.ChangeHealth(healAmount);
    }
    public void GetPushedBack(Vector3 pushedFrom, float pushBackForce, float pushBackDuration)
    {
        unitCombat.GetPushedBackFrom(pushedFrom,pushBackForce,pushBackDuration);
    }
    public void GetPushedBackInDirection(float directionAngle, float pushBackForce, float pushBackDuration)
    {
        unitCombat.GetPushedBackInDirection(directionAngle,pushBackForce,pushBackDuration);
    }
    public void GetStunned(float time)
    {
        unitCombat.GetStunned(time);
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
