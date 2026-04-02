using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [SerializeField] private Grenade _grenadePrefab;
    public override void ClickAttack()
    {
        if (!CanAttack())
            return;
        coolDownTimer = maxCooldown;

        Grenade grenade = Instantiate(_grenadePrefab);
        grenade.damage = ReturnCalculateDamage(out grenade.isCrit);
        grenade.transform.position = transform.position;
        grenade.CalcBalistics(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        GlobalEventManager.instance.TriggerOnPlayerAtack();
    }
}
