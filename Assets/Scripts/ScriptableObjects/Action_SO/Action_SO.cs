using UnityEngine;

//[CreateAssetMenu(fileName = "Action_SO", menuName = "Scriptable Objects/Action_SO")]
public class Action_SO : ScriptableObject
{
    public Projectile projectilePrefab;
    public virtual void Attack()
    {

    }
    public virtual void Attack(Unit sourceUnit)
    {

    }
    public virtual void Attack(Projectile projectilePrefab, Unit sourceUnit, Transform shootRotation)
    {

    }
    public virtual void Attack(Projectile projectilePrefab, Unit sourceUnit, Quaternion shootDirection)
    {

    }
    /*public virtual void Attack(Projectile projectilePrefab, Unit sourceUnit, Transform shootRotation, Quaternion shootDirection)
    {

    }*/
    public virtual void Attack(Unit sourceUnit, Transform shootRotation, Quaternion shootDirection)
    {

    }
    public virtual void DoOnTouch(Unit sourceUnit)
    {

    }
}
