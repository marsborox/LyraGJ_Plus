using UnityEngine;
using System.Collections;

public class PlayerCombat : UnitCombat
{
    public Weapon weapon1;
    public Weapon weapon2;
    public Weapon weapon3;
    public Weapon weapon4;

    public Weapon weapon1New;
    public Weapon weapon2New;
    public Weapon weapon3New;
    public Weapon weapon4New;

    public void Weapon1_OnClick()
    {
        // Debug.Log("playerCombat.wpn1 attack");
        MySoundManager.instance.HandleInstrument(MySoundManager.Instrument.Guitar);

        weapon1New.ClickAttack();
    }
    public void Weapon1_OnHold()
    {
        weapon1New.HoldAttack();
    }
    public void Weapon2_OnClick() 
    {
        // Debug.Log("playerCombat.wpn2 attack");
        MySoundManager.instance.HandleInstrument(MySoundManager.Instrument.Piano);

        weapon2New.ClickAttack();
    }
    public void Weapon2_OnHold()
    {
        weapon2New.HoldAttack();
    }
    public void Weapon3_OnClick()
    { 
        MySoundManager.instance.HandleInstrument(MySoundManager.Instrument.Saxophone);

        weapon3New.ClickAttack();
    }
    public void Weapon4_OnClick()
    {
        weapon4New.ClickAttack();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        MySoundManager.instance.PlayEnemyHit();

        if (healthCurrent <= 0)
        {
            Die();
        }
    }
    
    public override void SetHealthBar()
    { 
    
    }
    private void Die()
    {
        Time.timeScale = 0f;
        Debug.Log("player died");
        //MySceneManager.instance.OpenLobbyScene();
        GlobalEventManager.instance.TriggerOnPlayerDied();
        animationController.animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    
}
