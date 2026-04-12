using UnityEngine;
using System.Collections;
public class EnemyPlaceholder : MonoBehaviour
{
    [SerializeField] private Type placeholderType;
    [SerializeField] private float health;

    [Header("Damage Number")]
    [SerializeField] private DamageNumber damageNumberPrefab;
    [SerializeField] private Color damageNumberColorNoCrit = new Color32(252,112,2,255);
    [SerializeField] private Color damageNumberColorCrit = Color.green;
    
    [Header("Visuals")]
    [SerializeField] private SpriteRenderer topRenderer;
    [SerializeField] private SpriteRenderer bottomRenderer;
    [SerializeField] private SpriteRenderer fallenRenderer;
    [SerializeField] private Collider2D placeholderCollider;

    void Start()
    {
        UpdateColors();
    }
    void OnValidate()
    {
        UpdateColors();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        bool isCrit = false;
        float realDamage = 0;

        WeaponCollider weaponCollider = collision.gameObject.GetComponent<WeaponCollider>();
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();

        if (weaponCollider != null)
        {
            Weapon weapon = weaponCollider.weaponIBelongTo;
            if (weapon != null) 
            {
                float damage = weapon.ReturnCalculateDamage(out isCrit);
                realDamage = placeholderType == weapon.weaponType ? damage : 0;
            }
        }
        else if (projectile != null)
        {
            isCrit = projectile.isCrit;
            realDamage = placeholderType == projectile.projectileType ? projectile.damage : 0;
        }
        else
        {
            return; // something else interracted with placeholder
        }

        if (realDamage > 0)
        {
            MySoundManager.instance.PlayLyraHit();
            
            health -= realDamage;
            if (health <= 0)
            {
                StartCoroutine(FallDown(0.2f));
            }
        }
        ShowDamage(realDamage, isCrit);
    }
    private IEnumerator FallDown(float duration)
    {
        float elapsed = 0;
        Vector3 startScale = transform.localScale;
        Vector3 fallScale = new Vector3(startScale.x, 0.5f, startScale.z);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float easedT = t * t;

            transform.localScale = Vector3.Lerp(startScale, fallScale, easedT);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localScale = fallScale;

        fallenRenderer.enabled = true;
        topRenderer.enabled = false;
        bottomRenderer.enabled = false;
        placeholderCollider.enabled = false;
    }
    private void ShowDamage(float amount, bool isCrit)
    {
        Vector3 offset = new Vector3(0, 1f, 0);
        DamageNumber damageNumber = Instantiate(damageNumberPrefab, transform.position + offset, Quaternion.identity);
        damageNumber.Show(amount, isCrit ? damageNumberColorCrit : damageNumberColorNoCrit);
    }
    private void UpdateColors()
    {
        Color color = Color.white;

        switch (placeholderType)
        {
            case Type.RED:
                color = new Color(1, 0.6101415f, 0.5518868f);
                break;
            case Type.GREEN:
                color = new Color(0.684972f, 0.9433962f, 0.5829477f);
                break;
            case Type.BLUE:
                color = new Color(0.5330188f, 0.8658846f, 1);
                break;
        }

        topRenderer.color = color;
        bottomRenderer.color = color;
        fallenRenderer.color = color;
    }
}
