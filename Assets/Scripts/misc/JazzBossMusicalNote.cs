using UnityEngine;
using System.Collections;
public class JazzNote : MonoBehaviour
{
    public float jazzBoost; // how much it improves the player's jazz skills

    [Header("Idle Animation")]
    [SerializeField] private float bpm = 170f;
    [SerializeField] private float intensity = 0.5f;
    [SerializeField] private GameObject visualGameObject;

    [Header("Fly to Target Animation")]
    [SerializeField] private float flySpeed = 25f;
    [SerializeField] private float flyDuration = 1.3f;
    public Transform target;

    [Header("Damage Number")]
    [SerializeField] private DamageNumber damageNumberPrefab;
    [SerializeField] private Color damageNumberColorNoCrit = new Color32(252,112,2,255);
    [SerializeField] private Color damageNumberColorCrit = Color.green;

    [Header("Spotlight")]
    [SerializeField] private SpriteRenderer noteRenderer;
    public SpotlightChangingColors spotlight; // to calculate jazz boost

    private Coroutine _danceRoutine;

    void Start() {
        _danceRoutine = StartCoroutine(DanceRoutine(bpm, intensity));

        spotlight.colorHasChanged += ChangeColor;
        ChangeColor();
    }
    void OnDestroy()
    {
        spotlight.colorHasChanged -= ChangeColor;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_danceRoutine == null) return; // was already sent to boss

        bool isCrit = false;
        Type weaponNeeded = spotlight.CurrentColorType();

        WeaponCollider weaponCollider = collision.gameObject.GetComponent<WeaponCollider>();
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();

        if (weaponCollider != null)
        {
            Weapon weapon = weaponCollider.weaponIBelongTo;
            if (weapon != null) 
            {
                float damage = weapon.ReturnCalculateDamage(out isCrit);
                jazzBoost = weaponNeeded == weapon.weaponType ? damage : 0;
            }
        }
        else if (projectile != null)
        {
            isCrit = projectile.isCrit;
            jazzBoost = weaponNeeded == projectile.projectileType ? projectile.damage : 0;
        }
        else
        {
            return; // something else interracted with note
        }

        StopCoroutine(_danceRoutine);
        _danceRoutine = null;

        ShowDamage(jazzBoost, isCrit);
        StartCoroutine(FlyToTarget(target, flySpeed));
    }

    private void ChangeColor()
    {
        noteRenderer.color = spotlight.currentColor;
    }
    private void ShowDamage(float amount, bool isCrit)
    {
        Vector3 offset = new Vector3(0, 1f, 0); // to start just above note
        DamageNumber damageNumber = Instantiate(damageNumberPrefab, transform.position + offset, Quaternion.identity);
        damageNumber.Show(amount, isCrit ? damageNumberColorCrit : damageNumberColorNoCrit);
    }
    private IEnumerator DanceRoutine(float bpm, float intensity)
    {
        float beatDuration = 60f / bpm;
        float timer = 0f;

        while (true)
        {
            timer += Time.deltaTime;
            float progress = (timer % beatDuration) / beatDuration;
            float offset = Mathf.Sin(progress * Mathf.PI * 2f) * intensity;
            visualGameObject.transform.localPosition = new Vector3(0, offset, 0);

            yield return null;
        }
    }
    private IEnumerator FlyToTarget(Transform target, float speed)
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = target.position;
        
        float distance = Vector2.Distance(startPos, endPos);
        float duration = distance / speed; 
        float arcHeight = distance * 0.2f;
        
        float elapsed = 0;
        Vector2 direction = (endPos - startPos).normalized;
        Vector2 perpendicular = new Vector2(-direction.y, direction.x);
        Vector2 controlPoint = (startPos + endPos) / 2f + (perpendicular * arcHeight);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            transform.position = Mathf.Pow(1 - t, 2) * startPos +
                            2 * (1 - t) * t * controlPoint +
                            Mathf.Pow(t, 2) * endPos;

            yield return null;
        }
        transform.position = endPos;
    }
}
