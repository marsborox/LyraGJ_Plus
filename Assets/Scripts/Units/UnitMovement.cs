using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public enum Direction { UP, DOWN, LEFT, RIGHT }

    public Direction currentDirection = Direction.LEFT;

    public DirectionMovement currentUnitVisual;
    public DirectionMovement goingUp;
    public DirectionMovement goingDown;
    public DirectionMovement goingLeft;

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
        DisableAllVisuals();
        switch (currentDirection)
        {
            case Direction.UP:
                {
                    goingUp.gameObject.SetActive(true);
                    currentUnitVisual = goingUp;
                    playerRotation = 0f;
                    break;
                }
            case Direction.DOWN:
                {
                    goingDown.gameObject.SetActive(true);
                    currentUnitVisual = goingDown;
                    playerRotation = 0f;
                    break;
                }
            case Direction.LEFT:
                {
                    goingLeft.gameObject.SetActive(true);
                    currentUnitVisual = goingLeft;
                    playerRotation = 0f;
                    break;
                }
            case Direction.RIGHT:
                {
                    goingLeft.gameObject.SetActive(true);
                    currentUnitVisual = goingLeft;
                    playerRotation = 180f;
                    break;
                }
        }
        //weapons.transform.rotation = Quaternion.Euler(visual.x, playerRotation, visual.z);
        currentUnitVisual.transform.rotation = Quaternion.Euler(visual.x, playerRotation, visual.z);
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
}
