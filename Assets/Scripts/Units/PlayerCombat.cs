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
    private FMODUnity.StudioEventEmitter emitter;

    private void Awake()
    {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    public void Weapon1_OnClick()
    {
        // Debug.Log("playerCombat.wpn1 attack");
        if (emitter != null) {
            emitter.SetParameter("Piano_attack", 1);
            StartCoroutine(StopAttack("Piano_attack"));
        }

        weapon1New.ClickAttack();
    }
    public void Weapon1_OnHold()
    {
        weapon1New.HoldAttack();
    }
    public void Weapon2_OnClick() 
    {
        // Debug.Log("playerCombat.wpn2 attack");
        if (emitter != null) {
            emitter.SetParameter("Saxophone_attack", 1);
            StartCoroutine(StopAttack("Saxophone_attack"));
        }

        weapon2New.ClickAttack();
    }
    public void Weapon2_OnHold()
    {
        weapon2New.HoldAttack();
    }
    public void Weapon3_OnClick()
    { 
        weapon3New.ClickAttack();
    }
    public void Weapon4_OnClick()
    {
        weapon4New.ClickAttack();
    }
    public void AttackWeapon1()
    {
        if (weapon1.CanAttack())
        {
            MySoundManager.instance.PlayGuitar();
            weapon1.Attack();
        }
    }
    public void AttackWeapon2()
    {
        if (weapon2.CanAttack())
        {
            MySoundManager.instance.PlayPiano();
            weapon2.Attack();
        }
    }
    public void AttackWeapon3()
    {
        if (weapon3.CanAttack())
        {
            MySoundManager.instance.PlaySaxofone();
            weapon3.Attack();
        }
    }
    public void AttackWeapon4()
    {
        /*if (weapon4.CanAttack())
        {
            //MySoundManager.instance.PlaySaxofone();
            weapon4.Attack();
        }*/
    }

    IEnumerator StopAttack(string attackName)
    {
        yield return new WaitForSeconds(0.5f);
        emitter.SetParameter(attackName, 0f);
    }
}
