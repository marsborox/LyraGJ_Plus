using UnityEngine;

public class PlayerMovement : UnitMovement
{

    [SerializeField] private MouseFollow _mouseFollow;

    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCooldownTimer;
    [SerializeField] private float _dashCooldownTime;
    [SerializeField] private bool _isDashing=false;
    [SerializeField] private bool _isDashReady = true;
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
    private void FixedUpdate()
    {
        DoDashing();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            _isDashing = false;
        }
    }
    public void Move(Vector2 rawInput)
    {
        if (!canMove)
            return;
        //Vector3 delta = (rawInput * movementSpeed * Time.deltaTime);
        //transform.position += delta;
        //_myRigidbody2D.linearVelocity = delta;
        _myRigidbody2D.MovePosition(_myRigidbody2D.position + rawInput * movementSpeed * Time.fixedDeltaTime);
        currentUnitVisual.Animate(Time.deltaTime);
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
    /*public void DashMouse()
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
    }*/
    /*private Vector3 ReturnDashPosition(Vector3 mousePos)
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
    }*/
    public void DoDashing()
    {
        if (_isDashing)
        {
            //Debug.Log("is Dashing");
            //doing movement if 
            /*Vector3 delta = (_dashDestination * dashSpeed * Time.deltaTime);
            _myRigidbody2D.linearVelocity = delta;*/
            //_myRigidbody2D.MovePosition(_myRigidbody2D.position + (Vector2)_dashDestination * movementSpeed * Time.fixedDeltaTime);
            //_myRigidbody2D.MovePosition(_dashDestination * dashSpeed * Time.fixedDeltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _dashDestination, _dashSpeed*Time.deltaTime);
            float distance = Vector3.Distance(transform.position, _dashDestination);
            if (distance < 0.01)
            {
                _isDashing = false;
            }
        }
    }
}
