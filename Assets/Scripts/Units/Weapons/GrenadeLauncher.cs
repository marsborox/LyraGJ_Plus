using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [SerializeField] private Grenade _grenadePrefab;
    public override void ClickAttack()
    {
        Grenade grenade = Instantiate(_grenadePrefab);
        grenade.damage = ReturnCalculateDamage();
        grenade.transform.position = transform.position;
        grenade.CalcBalistics(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    }
}
