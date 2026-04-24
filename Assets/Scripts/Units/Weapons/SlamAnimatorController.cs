using UnityEngine;

public class SlamAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _slamAnimator;
    //[SerializeField] private AnimationClip _slamAnimation;


    int _slamAnimationStateHash;
    int _idleAnimationStateHash;

    void Start()
    {
        _slamAnimationStateHash = /*_slamAnimator.StringTo*/ Animator.StringToHash("Base Layer.SlamAnimation");
        _idleAnimationStateHash = Animator.StringToHash("Base Layer.SlamDefaultState");
    }

    public void PlaySlamAnimation()
    {
        Debug.Log("playing slam animation");
        _slamAnimator.Play(_slamAnimationStateHash);
    }
    public void SetDefaultAnimatorState()
    {
        Debug.Log("setting default animation state");
        _slamAnimator.Play(_idleAnimationStateHash);
    }
}
