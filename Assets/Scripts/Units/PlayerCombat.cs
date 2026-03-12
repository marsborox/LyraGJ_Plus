using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombat : UnitCombat
{
    /*public Weapon weapon1;
    public Weapon weapon2;
    public Weapon weapon3;
    public Weapon weapon4;*/

    public Weapon weapon1New;
    public Weapon weapon2New;
    public Weapon weapon3New;
    public Weapon weapon4New;
    public GameObject hitEffectPrefab;

    public List<Weapon> weaponList = new List<Weapon>();
    private void Start()
    {
        base.Start();
        
    }
    public void Weapon1_OnClick()
    {
        // Debug.Log("playerCombat.wpn1 attack");
        MySoundManager.instance.HandleInstrument(MySoundManager.Instrument.Guitar);

        SpawnMusicalNotes(new Color(1, 0, 0));
        animationController.HandleMeeleeAttackAnimation();
        weapon1New.ClickAttack();
    }
    public void Weapon2_OnClick() 
    {
        // Debug.Log("playerCombat.wpn2 attack");
        MySoundManager.instance.HandleInstrument(MySoundManager.Instrument.Piano);

        SpawnMusicalNotes(new Color(0, 1, 0));
        animationController.HandleRangedAttackAnimation();
        weapon2New.ClickAttack();
    }
    public void Weapon3_OnClick()
    { 
        MySoundManager.instance.HandleInstrument(MySoundManager.Instrument.Saxophone);

        SpawnMusicalNotes(new Color(0, 0, 1));
        animationController.HandleAoEAttackAnimation();
        weapon3New.ClickAttack();
    }
    public void Weapon4_OnClick()
    {
        SpawnMusicalNotes(new Color(1, 1, 1));
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
    private void SpawnMusicalNotes(Color color)
    {
        if (hitEffectPrefab == null) return;

        GameObject fx = Object.Instantiate(hitEffectPrefab, transform.localPosition, Quaternion.identity);
        fx.transform.position +=  new Vector3(0f, 1.2f, 0f);

        ParticleSystem ps = fx.GetComponent<ParticleSystem>();

        if (ps != null)
        {
            var main = ps.main;
            // main.startColor = color; // TODO: once we get nicer colors for weapons

            ps.Play();
            Object.Destroy(fx, ps.main.duration + ps.main.startLifetime.constantMax);
        }
    }
}
