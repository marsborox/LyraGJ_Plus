using UnityEngine;
[CreateAssetMenu(fileName = "SingleShot_SO", menuName = "Scriptable Objects/Action_SO/SingleShot_SO")]
public class SingleShot_SO : Action_SO
{
    public override void Attack(/*Projectile projectilePrefab,*/ Unit sourceUnit, Transform shootRotation, Quaternion shootDirection)
    {
        //Debug.Log("shot from SingleShot_SO");
        Projectile projectile;
        projectile = Instantiate(projectilePrefab, sourceUnit.transform.position, shootDirection);
        projectile.damage = sourceUnit.ReturnDamageAmount();
        //projectile.targetTag = sourceUnit.targetTag;
        projectile.sourceUnit = sourceUnit;
        //projectile.transform.SetParent(sourceUnit.transform); so bullet will move but will add source GO movement
    }
}
