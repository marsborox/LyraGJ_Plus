using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBehavior_SO", menuName = "Scriptable Objects/EnemyBehavior_SO")]
public class EnemyBehavior_SO : ScriptableObject
{
    public Action_SO action;
    public virtual void PerformBehavior(Unit source, Unit target)
    { 
        //something something ATTACK
    }
}
