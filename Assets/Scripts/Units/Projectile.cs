using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Unit sourceUnit;
    public string targetTag;
    [SerializeField] public float movementSpeed = 8f;
    public float damage;

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
            other.GetComponent<Unit>().TakeDamage(damage);
        }
    }
    public void FixedUpdate()
    {
        BulletMovement();
    }
    public void BulletMovement()
    {
        transform.Translate(Vector3.up * Time.fixedDeltaTime * movementSpeed);
    }
}
