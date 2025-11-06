using UnityEngine;

[CreateAssetMenu(fileName = "SpreadShot_SO", menuName = "Scriptable Objects/Action_SO/SpreadShot_SO")]

public class SpreadShot_SO : Action_SO
{
    [Tooltip("dont use higher number than 360")]
    [SerializeField] private float _spreadAngle = 30f;
    [SerializeField] int _numberOfShots = 5;
    public override void Attack(/*Projectile projectilePrefab,*/ Unit sourceUnit, Transform shootRotation, Quaternion shootDirection)
    {
        //Debug.Log("shot from SpreadShot_SO");
        int angles = _numberOfShots - 1;
        float angleBetweenTwoProjectiles = _spreadAngle / (_numberOfShots - 1);
        float attackDirectionInDegree = shootDirection.eulerAngles.z;

        float angleOfShot = attackDirectionInDegree - (_spreadAngle / 2);

        for (int i = 0; i < _numberOfShots; i++)
        {
            Quaternion directionOfProjectile = Quaternion.Euler(shootDirection.x, shootDirection.y, angleOfShot);
            Projectile projectile;
            projectile = Instantiate(projectilePrefab, sourceUnit.transform.position, directionOfProjectile);
            projectile.damage = sourceUnit.ReturnDamageAmount();
            //projectile.targetTag = sourceUnit.targetTag;
            projectile.sourceUnit = sourceUnit;
            //projectile.transform.SetParent(sourceUnit.transform); //so bullet will move but will add source GO movement
            angleOfShot += angleBetweenTwoProjectiles;
        }
    }

}
