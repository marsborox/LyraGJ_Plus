using UnityEngine;
using UnityEngine.UI;

public class AttackImitation : MonoBehaviour
{
    [SerializeField] private Image _animation;
    [SerializeField] private Image _cooldown;

    [SerializeField] private EnemyCombat _enemyCombat;

    private void Update()
    {
        DoAttackAnimation();
        ShowCooldown();


    }
    public void DoAttackAnimation()
    {
        float fraction = 0.3f;
        fraction =  _enemyCombat.attackAnimationTime/ _enemyCombat.attackAnimationTimer;
        _animation.fillAmount = (float)(_enemyCombat.attackAnimationTimer / _enemyCombat.attackAnimationTime);
    }
    public void ShowCooldown()
    {
        float fraction = 0.3f;
        fraction = _enemyCombat.attackCooldown/ _enemyCombat.coolDownTimer;
        _cooldown.fillAmount = (float)(_enemyCombat.coolDownTimer/_enemyCombat.attackCooldown);

    }
}
