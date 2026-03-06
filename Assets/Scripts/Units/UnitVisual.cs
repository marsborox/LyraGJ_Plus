using UnityEngine;
using UnityEngine.U2D.Animation;

public class UnitVisual : MonoBehaviour
{
    [SerializeField] private UnitCombat _unitCombat;
    
    private void Start()
    {
        
    }
    
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
        //Debug.Log("post takeDamage post animation");
        _unitCombat.isStunned = false;
    }

    public void PostDeathAnimationEvent()
    {
        _unitCombat.Die();
    }
}
