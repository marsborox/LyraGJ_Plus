using UnityEngine;

public class PlayerAnimationController : UnitAnimationController
{
    private void FixedUpdate()
    {
        HandleMovementAnimationIdle();
    }
}
