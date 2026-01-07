using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public enum Direction { UP, DOWN, LEFT, RIGHT }

    public Direction currentDirection = Direction.LEFT;

    public DirectionMovement currentUnitVisual;
    public DirectionMovement goingUp;
    public DirectionMovement goingDown;
    public DirectionMovement goingLeft;
    public UnitAnimationController animationController;
    public GameObject weapons;
    public float movementSpeed = 100f;
    public bool canMove = true;
    public bool canDash = true;
    public void Update()
    {
        FaceCorrectDirection();
    }
    public void FaceCorrectDirection()
    {
        var visual = transform.rotation;
        float playerRotation = 0;
    }
    void DisableAllVisuals()
    {
        goingUp.gameObject.SetActive(false);
        goingDown.gameObject.SetActive(false);
        goingLeft.gameObject.SetActive(false);
    }
    public void CanMove()
    {
        canMove = true;
        canDash = true;
    }
    public void CanNotMove()
    {
        canMove = false;
        canDash = false;
    }
    public virtual void DirectionAngleSnapped()
    {


    }
    public void StayIdle()
    {
        animationController.HandleMovementAnimationIdle();
    }
}
