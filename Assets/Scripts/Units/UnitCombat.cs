using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitCombat : MonoBehaviour
{
    public int healthMax = 10;
    public int healthCurrent;
    public float healthFraction;
    public Image healthBar;

    public string tagThatHitsUs;
    public string projectileTagThatHitsUs;
    public string projectileTagWeShot;

    public bool isPushedBack = false;
    public float pushBackTime = 0.2f;
    //public float pushBackForce = 10f;

    public Rigidbody2D myRigidBody;
    void Start()
    {
        healthCurrent = healthMax;
    }
    public void Update()
    {
        SetHealthBar();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("GotHitBySomething");
        GetHitFromWeapon(other);
        GetHitFromExplosion(other);
        GetHitFromProjectile(other);
    }
    public void GetHitFromWeapon(Collider2D other)
    {
        if (other.gameObject.tag == tagThatHitsUs)
        {
            //Debug.Log("collision w weapon");
            Weapon weapon = other.gameObject.GetComponent<WeaponCollider>().weaponIBelongTo;
            //AnalyseAndTakeDamage(weapon);
            Debug.Log("got hit by enemyWeapon");

        }
    }
    private void GetHitFromExplosion(Collider2D other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            //get damage
            Explosion explosion = other.gameObject.GetComponent<Explosion>();
            Debug.Log("got hit by explosion");
        }
    }
    private void GetHitFromProjectile(Collider2D other)
    {
        if (other.gameObject.tag == projectileTagThatHitsUs)
        {
            //get damage
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            Debug.Log("got hit by enemyProjectile");

            //get knocked back
            //get effect
        }
    }
    void SetHealthBar()
    {
        healthFraction = (float)healthCurrent / (float)healthMax;
        healthBar.fillAmount = healthFraction;
    }
    public void TakeDamage(int damage)
    {
        healthCurrent -= damage;
        //Debug.Log(damage+" damage taken");
    }
    public void GetPushedBack(Vector3 pushedFrom,float pushBackForce, float pushBackDuration)
    {
        if (isPushedBack)
            return;
        Vector2 difference = (transform.position - pushedFrom).normalized * pushBackForce * myRigidBody.mass;
        myRigidBody.AddForce(difference, ForceMode2D.Impulse);
        isPushedBack = true;
        StartCoroutine(PushBackRoutine(pushBackDuration));
    }
    IEnumerator PushBackRoutine(float pushBackDuration)
    {
        yield return new WaitForSeconds(pushBackDuration);
        isPushedBack = false;
        myRigidBody.linearVelocity = Vector3.zero;
    }
}
