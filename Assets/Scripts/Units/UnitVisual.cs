using UnityEngine;

public class UnitVisual : MonoBehaviour
{
    [SerializeField] private UnitCombat _unitCombat;

    public void PostAttackAnimationEvent()
    {
        //Debug.Log("PostAttackAnimationEventUnit in UnitVisual");
        _unitCombat.PostAttackAnimationEventUnit();
    }
    /*public void PostAttackAnimationEvent(string str)
    {
        Debug.Log("PostAttackAnimationEventUnit in UnitVisual");
        Debug.Log(str);
        //_unitCombat.PostAttackAnimationEventUnit();
    }*/
    public void PostTakeDamageAnimationEvent()
    {
        _unitCombat.isStunned = false;
    }
    public void PostGetHitAnimationEvent()
    { 
        _unitCombat.isStunned = false;
    }
    public void PostDeathAnimationEvent()
    {
        _unitCombat.Die();
    }
}
