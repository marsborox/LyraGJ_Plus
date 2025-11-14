using UnityEngine;
using UnityEngine.UI;

public class UnitCombat : MonoBehaviour
{
    public int healthMax = 10;
    public int healthCurrent;
    public float healthFraction;
    public Image healthBar;
    void Start()
    {
        healthCurrent = healthMax;
    }
    public void Update()
    {
        SetHealthBar();
    }
    void SetHealthBar()
    {
        healthFraction = (float)healthCurrent / (float)healthMax;
        healthBar.fillAmount = healthFraction;
    }
    public void TakeDamage(int damage)
    {
        healthCurrent -= damage;
        //Debug.Log(damage+" damage taken");
    }
}
