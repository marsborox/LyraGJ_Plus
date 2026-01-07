using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    public Animator animator;


    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private UnitMovement _unitMovement;
        
    public bool isMoving = false;
    private Vector2 _lastMoveDirection = Vector2.down;// will default to down when game starts
    private void Start()
    {

    }


    public void HandleAnimationNoIdle()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //for some reason axes are messed up when we added (UnitMovement.Direction)
        //thats why this logic is funky
        float horizontal = 0;
        float vertical = 0;
        switch (_unitMovement.currentDirection)
        {
            case (UnitMovement.Direction)Direction.UP:
                {
                    horizontal = -1;
                    break;
                }
            case (UnitMovement.Direction)Direction.DOWN:
                {
                    horizontal = 1;
                    break;
                }
            case (UnitMovement.Direction)Direction.LEFT:
                {
                    vertical = 1;
                    break;
                }
            case (UnitMovement.Direction)Direction.RIGHT:
                {
                    vertical = -1;
                    break;
                }
        }

        animator.SetFloat("Xinput", horizontal);
        animator.SetFloat("Yinput", vertical);

        //_animator.SetFloat("Xinput", vertical);
        //_animator.SetFloat("Yinput", horizontal);
        //animator.SetFloat();

        if (horizontal > 0)
        {
            _spriteRenderer.flipX = true;
            //Debug.Log("facingLeft");
        }
        else
        {
            _spriteRenderer.flipX = false;
            //Debug.Log("facingRight");
        }
        //Debug.Log("Should be animating, direction X: "+horizontal.ToString() + " Y: "+vertical.ToString());
    }
    public void HandleAnimation/*AutoDIrectionCheck*/()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //for some reason axes are messed up when we added (UnitMovement.Direction)
        //thats why this logic is funky
        float horizontal = 0;
        float vertical = 0;
        switch (_unitMovement.currentDirection)
        {
            case (UnitMovement.Direction)Direction.UP:
                {
                    horizontal = -1;
                    break;
                }
            case (UnitMovement.Direction)Direction.DOWN:
                {
                    horizontal = 1;
                    break;
                }
            case (UnitMovement.Direction)Direction.LEFT:
                {
                    vertical = 1;
                    break;
                }
            case (UnitMovement.Direction)Direction.RIGHT:
                {
                    vertical = -1;
                    break;
                }
        }
        Vector2 movement = new Vector2(horizontal, vertical);

        if (horizontal > 0)
        {
            _spriteRenderer.flipX = true;
            //Debug.Log("facingLeft");
        }
        else
        {
            _spriteRenderer.flipX = false;
            //Debug.Log("facingRight");
        }
        if (movement != Vector2.zero) 
        {//this should check if we ahve moved
            _lastMoveDirection = movement;
        }
        //Debug.Log("Should be animating, direction X: " + horizontal.ToString() + " Y: " + vertical.ToString());
        //Debug.Log("velocity is "+movement);


        animator.SetFloat("Xinput", _lastMoveDirection.x);
        animator.SetFloat("Yinput", _lastMoveDirection.y);
        animator.SetFloat("Speed", movement.magnitude);

        //_animator.SetFloat("Xinput", vertical);
        //_animator.SetFloat("Yinput", horizontal);
        //animator.SetFloat();
    }
    
    public void HandleMovementAnimation()
    {
        float horizontal = 0;
        float vertical = 0;

        Vector2 movement = new Vector2(horizontal, vertical);

        if (movement != Vector2.zero)
        {//this should check if we ahve moved
            return;
        }
        //_lastMoveDirection = movement;
        //Debug.Log("Should be animating, direction X: " + horizontal.ToString() + " Y: " + vertical.ToString());
        //Debug.Log("velocity is " + movement);
        animator.SetFloat("Xinput", _lastMoveDirection.x);
        animator.SetFloat("Yinput", _lastMoveDirection.y);
        animator.SetFloat("Speed", movement.magnitude);
    }
    public void HandleMovementAnimationEnemy()
    {
        Vector2 vector = new Vector2(0, 0);
        HandleMovementAnimation(vector);
    }

    public void HandleMovementAnimation/*AutoDIrectionCheck*/(Vector2 inputVector)
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //for some reason axes are messed up when we added (UnitMovement.Direction)
        //thats why this logic is funky
        float horizontal = 0;
        float vertical = 0;
        switch (_unitMovement.currentDirection)
        {
            case (UnitMovement.Direction)Direction.UP:
                {
                    horizontal = -1;
                    break;
                }
            case (UnitMovement.Direction)Direction.DOWN:
                {
                    horizontal = 1;
                    break;
                }
            case (UnitMovement.Direction)Direction.LEFT:
                {
                    vertical = 1;
                    break;
                }
            case (UnitMovement.Direction)Direction.RIGHT:
                {
                    vertical = -1;
                    break;
                }
        }
        Vector2 movement = new Vector2(horizontal, vertical);

        
        if (horizontal > 0)
        {
            _spriteRenderer.flipX = true;
            //Debug.Log("facingLeft");
        }
        else
        {
            _spriteRenderer.flipX = false;
            //Debug.Log("facingRight");
        }
        if (movement != Vector2.zero)
        {//this should check if we ahve moved
            _lastMoveDirection = movement;
        }
        //Debug.Log("Should be animating, direction X: " + horizontal.ToString() + " Y: " + vertical.ToString());
        //Debug.Log("velocity is "+movement);


        animator.SetFloat("Xinput", _lastMoveDirection.x);
        animator.SetFloat("Yinput", _lastMoveDirection.y);
        animator.SetFloat("Speed", movement.magnitude);

        //_animator.SetFloat("Xinput", vertical);
        //_animator.SetFloat("Yinput", horizontal);
        //animator.SetFloat();
    }
    public void HandleEnemyAttackAnimation()
    {
        Debug.Log("Attacking Animation");
        animator.SetTrigger("Attack");
    }
    public void HandleTakeDamageAnimation()
    {
        Debug.Log("Taking damage Animation");
        animator.SetTrigger("TakeDamage");
    }
}
