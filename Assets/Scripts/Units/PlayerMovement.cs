using UnityEngine;
using UnityEngine.UIElements;

using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : UnitMovement
{
    
    [SerializeField] private MouseFollow _mouseFollow;
    [SerializeField] private PlayerDashChecker _dashChecker;

    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCooldownTimer;
    [SerializeField] private float _dashCooldownTime;
    [SerializeField] private bool _isDashing=false;
    [SerializeField] private bool _isDashReady = true;
    [SerializeField] private bool _isTouchingWall=false;
    
    [SerializeField] private float _maxDashDistance;
    //[SerializeField] private float _maxDashTime;
    [SerializeField] private float _dashSpeedCoef;

    [SerializeField] private Vector3 _dashDestination;
    //[SerializeField] private Vector3 _mousePosVector;

    private Rigidbody2D _myRigidbody2D;


    private void Awake()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _dashSpeed = movementSpeed * _dashSpeedCoef;
    }
    public void Update()
    {
        DirectionAngleSnapped();
        base.Update();
        
    }
    private void FixedUpdate()
    {
        DoDashing();
        //HandleAnimation();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            _isDashing = false;
            _isTouchingWall=true;
            //canDash = false;
            //Debug.Log("playerMovement player hit wall");
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            _isTouchingWall = false;
            //canDash = true;

            //Debug.Log("playerMovement player hit wall");
        }
    }
    public void MoveByVector(Vector2 rawInput)
    {
        if (!canMove)
            return;
        //Vector3 delta = (rawInput * movementSpeed * Time.deltaTime);
        //transform.position += delta;
        //_myRigidbody2D.linearVelocity = delta;
        _myRigidbody2D.MovePosition(_myRigidbody2D.position + rawInput * movementSpeed * Time.fixedDeltaTime);
        //currentUnitVisual.Animate(Time.deltaTime);
    }
    public void MoveByMouse()
    {
        //Debug.Log("mouseMovement");
        if (!canMove)
            return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position,mousePosition);
        //Debug.Log("distance between mouse and player is: "+distance);
        // to avoid jitterying , because camera is following w delay
        if (distance < 0.8)
        { return; }
        //Debug.Log(mousePosition);
        //_myRigidbody2D.MovePosition(mousePosition /* * movementSpeed * Time.fixedDeltaTime*/);
        //_myRigidbody2D.MovePosition( (transform.position - mousePosition) * movementSpeed * Time.fixedDeltaTime/* - transform.position*/);

        Vector3 movePosition = (mousePosition - (Vector2)this.transform.position).normalized;
        
        //transform.position = Vector2.MoveTowards(transform.position, movePosition, movementSpeed*Time.fixedDeltaTime);
        _myRigidbody2D.MovePosition(transform.position + movePosition * movementSpeed * Time.fixedDeltaTime);
        animationController.HandleMovementAnimation();

        MySoundManager.instance.PlayFootsteps();
    }
    public void DashWSAD(Vector2 rawInput)
    {
        if (_isDashing || !canDash)
        {
            return;
        }
        _dashDestination = (Vector2)this.transform.position + rawInput.normalized*_maxDashDistance;
        //Debug.Log(input);
        _isDashing = true;
    }
    
    public void DoDashing()
    {
        if (!_isDashing) return;

        if (_isTouchingWall && _dashChecker.isPointingAtWall)
        {
            _isDashing = false;
            return;
        }
        
        //Debug.Log("is Dashing");
        //doing movement if 
        /*Vector3 delta = (_dashDestination * dashSpeed * Time.deltaTime);
        _myRigidbody2D.linearVelocity = delta;*/
        //_myRigidbody2D.MovePosition(_myRigidbody2D.position + (Vector2)_dashDestination * movementSpeed * Time.fixedDeltaTime);
        //_myRigidbody2D.MovePosition(_dashDestination * dashSpeed * Time.fixedDeltaTime);

            
        //********************************
        transform.position = Vector3.MoveTowards(transform.position, _dashDestination, _dashSpeed*Time.deltaTime);//rework dash
        _myRigidbody2D.MovePosition(transform.position - _dashDestination * _dashSpeed * Time.fixedDeltaTime);

        //transform.position = Vector3.MoveTowards(transform.position,)
        float distance = Vector3.Distance(transform.position, _dashDestination);
        if (distance < 0.01)
        {
            _isDashing = false;
        }
    }

    public void DashMouse()
    {
        if (_isDashing||!canDash) 
        {
            return;
        }
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //_mousePosVector = mousePos;//remove eventually
        float playerToMouseDistance = Vector2.Distance(this.transform.position, mousePos);
        //Debug.Log("playerToMouse distance is "+playerToMouseDistance);
        if (_maxDashDistance < playerToMouseDistance)
        {
            _dashDestination = ReturnDashPosition(mousePos);
        }
        else
        {
            _dashDestination = mousePos;
        }
        _isDashing = true;

        //take direction from mouse pos, if distance to mouse is greater than max dash distance
        //move to max distance, else move to mouse pos
        //Start dash cd
    }
    public void DashMouseNew()
    {
        if (_isDashing || !canDash)
        {
            return;
        }

    }
    private Vector3 ReturnDashPosition(Vector3 mousePos)
    {
        Vector3 destination;
        float angle = (_mouseFollow.transform.localEulerAngles.z+90)*Mathf.Deg2Rad;//90 bcs its rotated
        //Debug.Log("getting point at angle (rad) "+angle);
        float sinAngle = Mathf.Sin(angle);
        //Debug.Log("sin of angle is " + sinAngle);
        float xIncrement = Mathf.Cos(angle) * _maxDashDistance;
        float yIncrement = Mathf.Sin(angle) * _maxDashDistance;
        destination = new Vector3(transform.position.x+xIncrement, transform.position.y+yIncrement, transform.position.z);
        return destination;
    }
    public override void DirectionAngleSnapped()
    {
        //adjusted by 45 degree
        float directionAngle = (_mouseFollow.ReturnMouseDirectionAngle()) + 45;
        if (directionAngle > 360) directionAngle -= 360;
        //Debug.Log(directionAngle);
        if (directionAngle < 90)
        {
            currentDirection = Direction.UP;
        }
        else if (90 < directionAngle && directionAngle < 180)
        {
            currentDirection = Direction.LEFT;
        }
        else if (180 < directionAngle && directionAngle < 270)
        {
            currentDirection = Direction.DOWN;
        }
        else
        { 
            currentDirection= Direction.RIGHT;
        }
    }

}
