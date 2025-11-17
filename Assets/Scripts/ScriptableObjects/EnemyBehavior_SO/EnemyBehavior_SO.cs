using UnityEngine;

//[CreateAssetMenu(/*fileName = "EnemyBehavior_SO",*/ menuName = "Scriptable Objects/EnemyBehavior_SO")]
public class EnemyBehavior_SO : ScriptableObject
{
    public Action_SO action;
    public virtual void PerformBehavior(Unit source, Unit target)
    {
        //something something ATTACK
        Debug.Log("Behavior not implemented");
    }
    public virtual void PerformBehavior(EnemyCombat source, Unit target)
    {
        //something something ATTACK
        Debug.Log("Behavior not implemented");
    }
    public virtual void PostAttackAction(EnemyCombat source, Unit target)
    {
        Debug.Log("PostAttack action not implemented");
    }

}
