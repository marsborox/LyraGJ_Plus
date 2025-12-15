using Unity.VisualScripting;

using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    public Type shieldType;
    public SpriteRenderer shieldSprite;
    [SerializeField]private EnemyCombat _enemyCombat;
    public void OnTriggerEnter2D(Collider2D other)
    {
        //PlayerProjectile
        //PlayerWeapon
        if (other.gameObject.tag == "PlayerWeapon")
        {
            if (other.GetComponent<WeaponCollider>().weaponIBelongTo.weaponType == shieldType)
            {
                _enemyCombat.isShielded = false;
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == "PlayerProjectile")
        {
            var projectile= other.GetComponent<Projectile>();
            if (projectile.projectileType == shieldType)
            {
                _enemyCombat.isShielded = false;
                Destroy(gameObject);
            }
            else
            {
                Destroy(projectile.gameObject);
            }
        }


    }
    public void SetShieldType(Type type,Color color)
    {
        shieldType = type;
        shieldSprite.color = color;
    }
    public void WeaponHit()
    { 
        
    }
}
