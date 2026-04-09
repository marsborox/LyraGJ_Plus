using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Type projectileType;
    public Unit sourceUnit;
    public string targetTag;
    public float movementSpeed = 8f;
    public int damage;
    public bool isCrit=false;
    public float pushBackForce;
    public float pushBackDuration;
    public float miniStunDuration;
    public SpriteRenderer projectileSpriteRenderer;
    //we will hardcode that if player with anything hits projectile it gets destroyed
    
    
    private void Start()
    {
        //Debug.Log(this.gameObject.tag + " has been spawned");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {// adjust this
            Destroy(this.gameObject);
        }
        PlayerDestroyEnemyProjectile(other);
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
        //Debug.Log("projectile hit");
        // must pass rotation of this object
        //unit.GetPushedBack(this.transform.position,pushBackForce,pushBackDuration);
        if (unit is Enemy)
        {
            if (((EnemyCombat)unit.unitCombat).isShielded)
            {
                return;
            }
        }
        unit.GetStunned(miniStunDuration);
        unit.TakeDamage(damage, isCrit);
        Destroy(gameObject);
    }
    //if our target tag is player and this is jsut a proejctile and other tag is weapon or projectile of player destroy this

    void PlayerDestroyEnemyProjectile(Collider2D other)
    {
        if (!(this.gameObject.tag == "EnemyProjectile"))
        {
            return;
        }
        if (/*targetTag == "Player" &&*/ (other.gameObject.tag == "PlayerProjectile") || (other.gameObject.tag == "PlayerWeapon"))
        {
            //Debug.Log("EnemyProjectile destoryed by " + other.gameObject.tag);
            Destroy(this.gameObject);
        }
    }
}
