using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public UnitStats unitStats;
    public enum Direction { UP, DOWN, LEFT, RIGHT }

    public Direction currentDirection = Direction.LEFT;
    public DirectionMovement currentUnitVisual;
    public DirectionMovement goingUp;
    public DirectionMovement goingDown;
    public DirectionMovement goingLeft;

    public GameObject weapons;
    [SerializeField] private Image _healthBar;

    public int healthMax = 10;
    public int healthCurrent;

    public float healthFraction;

    public string targetTag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthCurrent = healthMax;
    }


    // Update is called once per frame
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
    public void FaceCorrectDirection()
    {
        var visual = transform.rotation;
        float playerRotation = 0;
        DisableAllVisuals();
        switch (currentDirection)
        {
            case Direction.UP:
                {
                    goingUp.gameObject.SetActive(true);
                    currentUnitVisual = goingUp;
                    playerRotation = 0f;
                    break;
                }
            case Direction.DOWN:
                {
                    goingDown.gameObject.SetActive(true);
                    currentUnitVisual = goingDown;
                    playerRotation = 0f;
                    break;
                }
            case Direction.LEFT:
                {
                    goingLeft.gameObject.SetActive(true);
                    currentUnitVisual = goingLeft;
                    playerRotation = 0f;
                    break;
                }
            case Direction.RIGHT:
                {
                    goingLeft.gameObject.SetActive(true);
                    currentUnitVisual = goingLeft;
                    playerRotation = 180f;
                    break;
                }

        }
        weapons.transform.rotation = Quaternion.Euler(visual.x, playerRotation, visual.z);
        currentUnitVisual.transform.rotation = Quaternion.Euler(visual.x, playerRotation, visual.z);
    }

    void DisableAllVisuals()
    {
        goingUp.gameObject.SetActive(false);
        goingDown.gameObject.SetActive(false);
        goingLeft.gameObject.SetActive(false);
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
    public float ReturnScoreAmount()
    {
        return unitStats.score;
    }
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
