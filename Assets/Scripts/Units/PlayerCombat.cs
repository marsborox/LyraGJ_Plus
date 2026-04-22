using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombat : UnitCombat
{
    public Weapon weaponGuitar;
    public Weapon weaponPiano;
    public Weapon weaponSax;
    public Weapon weapon4New;
    public GameObject hitEffectPrefab;
    public List<Weapon> weaponList = new List<Weapon>();

    private bool isAttacking = false;

    private Coroutine _attackCancelling;

    private void Start()
    {
        base.Start();   
    }
    public void OnTriggerEnter2DCustom(Collider2D other)
    {
        //Debug.Log("GotHitBySomething");
        GetHitFromWeapon(other);
        GetHitFromExplosion(other);
        GetHitFromProjectile(other);
    }
    public void Weapon1_OnClick()
    {
        if (isAttacking) return;
        isAttacking = true;

        // Debug.Log("playerCombat.wpn1 attack");
        MySoundManager.instance.PlayInstrument(MySoundManager.Instrument.Guitar);

        SpawnMusicalNotes(new Color(1, 0, 0));
        animationController.HandleMeeleeAttackAnimation();
        
        weaponGuitar.ClickAttack();
        
        CancelAttackIfNeeded();
    }
    public void Weapon2_OnClick() 
    {
        if (isAttacking) return;
        isAttacking = true;

        // Debug.Log("playerCombat.wpn2 attack");
        MySoundManager.instance.PlayInstrument(MySoundManager.Instrument.Piano);

        SpawnMusicalNotes(new Color(0, 1, 0));
        animationController.HandleRangedAttackAnimation();
        weaponPiano.ClickAttack();
        //Debug.Log("doing ranged attack");

        CancelAttackIfNeeded();
    }
    public void Weapon3_OnClick()
    { 
        if (isAttacking) return;
        isAttacking = true;

        MySoundManager.instance.PlayInstrument(MySoundManager.Instrument.Saxophone);

        SpawnMusicalNotes(new Color(0, 0, 1));
        animationController.HandleAoEAttackAnimation();
        weaponSax.ClickAttack();

        CancelAttackIfNeeded();
    }
    public void Weapon4_OnClick()
    {
        /*if (isAttacking) return;
        isAttacking = true;*/

        SpawnMusicalNotes(new Color(1, 1, 1));
        weapon4New.ClickAttack();
    }

    public override void TakeDamage(int damage, bool isCrit)
    {
        //Debug.Log("player taking damage: "+damage);
        isAttacking = false;
        base.TakeDamage(damage);

        MySoundManager.instance.PlayEnemyHit();
        if (healthCurrent <= 0)
        {
            Die();
            //Debug.Log("player died");
        }

    }
    public override void PostAttackAnimationEventUnit()
    {
        isAttacking = false;
    }

    private void CancelAttackIfNeeded()
    {
        if (_attackCancelling != null) StopCoroutine(_attackCancelling);
        _attackCancelling = StartCoroutine(AttackSafetyNet());
    }
    private IEnumerator AttackSafetyNet() {
        yield return new WaitForSeconds(0.5f); // longer than any attack Lyra has
        isAttacking = false;
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

        GameObject fx = Object.Instantiate(hitEffectPrefab, myRigidBody.transform.localPosition, Quaternion.identity);
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
