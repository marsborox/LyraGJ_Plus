using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Unit sourceUnit;
    public string targetTag;
    public float movementSpeed = 8f;
    public int damage;
    public float pushBackForce;
    public float pushBackDuration;
    public float miniStunDuration;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {// adjust this
            Destroy(gameObject);
        }
        else if (other.tag == targetTag)
        {

        }
        if (other.tag == targetTag)
        {

        }
    }
    public virtual void FixedUpdate()
    {
        BulletMovement();
    }
    public void BulletMovement()
    {
        transform.Translate(Vector3.up * Time.fixedDeltaTime * movementSpeed);
    }
    public void ProjectileHit(Unit unit)
    {
        
        unit.TakeDamage(damage);
        // must pass rotation of this object
        //unit.GetPushedBack(this.transform.position,pushBackForce,pushBackDuration);
        unit.GetStunned(miniStunDuration);
    }
}
