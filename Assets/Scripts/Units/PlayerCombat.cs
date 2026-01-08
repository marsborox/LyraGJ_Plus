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
    float pianoIntensity = 0f;
    float saxophoneIntensity = 0f;
    Coroutine pianoDecayRoutine;
    Coroutine saxophoneDecayRoutine;
    float pianoHoldTimer = 0f;
    float saxophoneHoldTimer = 0f;


    private void Awake()
    {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    public void Weapon1_OnClick()
    {
        // Debug.Log("playerCombat.wpn1 attack");
        if (emitter != null) {
            PlayPianoAccent();
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
            PlaySaxophoneAccent();
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
    public void PlayPianoAccent()
    {
        pianoHoldTimer = 0.5f;
        pianoIntensity += 0.8f;
        pianoIntensity = Mathf.Clamp01(pianoIntensity);

        emitter.SetParameter("Piano_attack", pianoIntensity);

        if (pianoDecayRoutine != null)
            StopCoroutine(pianoDecayRoutine);

        pianoDecayRoutine = StartCoroutine(PianoDecay());
    }
    IEnumerator PianoDecay()
    {
        while (pianoIntensity > 0f)
        {
            // float val;
            // emitter.EventInstance.getParameterByName("Piano_attack", out val);
            // Debug.Log("Piano_attack = " + val);

            if (pianoHoldTimer > 0f)
            {
                pianoHoldTimer -= Time.deltaTime;
            } else {
                pianoIntensity -= Time.deltaTime * 0.8f; // decay speed
                emitter.SetParameter("Piano_attack", pianoIntensity);
            }
            yield return null;
        }

        pianoIntensity = 0f;
    }
    public void PlaySaxophoneAccent()
    {
        saxophoneHoldTimer = 0.5f;
        saxophoneIntensity += 0.8f;
        saxophoneIntensity = Mathf.Clamp01(saxophoneIntensity);

        emitter.SetParameter("Saxophone_attack", saxophoneIntensity);

        if (saxophoneDecayRoutine != null)
            StopCoroutine(saxophoneDecayRoutine);

        saxophoneDecayRoutine = StartCoroutine(SaxophoneDecay());
    }
    IEnumerator SaxophoneDecay()
    {
        while (saxophoneIntensity > 0f)
        {
            // float val;
            // emitter.EventInstance.getParameterByName("Saxophone_attack", out val);
            // Debug.Log("Saxophone_attack = " + val);

            if (saxophoneHoldTimer > 0f)
            {
                saxophoneHoldTimer -= Time.deltaTime;
            } else {
                saxophoneIntensity -= Time.deltaTime * 0.8f; // decay speed
                emitter.SetParameter("Saxophone_attack", saxophoneIntensity);
            }
            yield return null;
        }
        saxophoneIntensity = 0f;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
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
    }
    
}
