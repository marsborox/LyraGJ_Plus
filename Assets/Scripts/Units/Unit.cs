using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    //[SerializeField] private SpriteRenderer _sprite; 
    public UnitStats unitStats;
    public UnitCombat unitCombat;
    public string targetTag;


    public void Start()
    {
        //_sprite.enabled = false;
    }
    public void Update()
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
        //Debug.Log("unit TakingDamage");
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
        ///Debug.Log("getting stun");
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
