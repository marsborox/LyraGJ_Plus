using System.Collections.Generic;
using System;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [System.Serializable]
    public class Stat
    {
        public string name;
        public float amount;
        public float upgradeAmount;
        Action upgradeMethod;
    }
    [Header("Stats")]
    //dsds
    [Header("DO NOT CHANGE NAMES")]
    public Stat healthMax_s;
    public Stat damage_s;
    public Stat movementSpeed_s;
    public Stat attackSpeed_s;
    public Stat attackRange_s;

    [Header(" ")]
    //public UnitStats unitStats;
    //[Tooltip("Health")]
    //public float healthMax = 1;
    //[Tooltip("Damage")]
    //public float damage = 1;
    //public float movementSpeed = 3;
    [Tooltip("Attack_Speed")]
    
    public float attackInterval;
    public float attackTimer = 0;
    [SerializeField] private float SpeedToIntervaL = 100;

    
    public List<float> statListFloat = new List<float>();
    public List<Stat> statList = new List<Stat>();
    public void Start()
    {
        CountAttackInterval();
        AddStatsToList();
    }
    void SetStats()
    {

    }
    public void CountAttackInterval()
    {
        attackInterval = SpeedToIntervaL / attackSpeed_s.amount;
        //Debug.Log(nameof(attackSpeed) + " " + attackSpeed);
    }
    private void AddStatsToList()
    {
        if (statList.Count == 0)
        {
            statList.Add(healthMax_s);
            statList.Add(damage_s);
            statList.Add(movementSpeed_s);
            statList.Add(attackSpeed_s);
        }
    }
    public float ReturnRandomStat()
    {
        /*
        float nbr = 1f;
        return nbr;*/

        int random = UnityEngine.Random.Range(0, statListFloat.Count - 1);
        return statListFloat[random];
    }

}
