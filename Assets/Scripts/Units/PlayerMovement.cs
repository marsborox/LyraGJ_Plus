using Unity.VisualScripting;

using UnityEditor;

using UnityEngine;

using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerMovement : UnitMovement
{

    [SerializeField] private MouseFollow _mouseFollow;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldownTimer;
    [SerializeField] private float dashCooldownTime;
    public bool isDashing=false;
    [SerializeField] private bool isDashReady = true;
    [SerializeField] private float maxDashDistance;
    [SerializeField] private float maxDashTime;
    [SerializeField] private float dashSpeedCoef;



    [SerializeField] private Vector3 _dashDestination;
    [SerializeField] private Vector3 _mousePosVector;

    private Rigidbody2D _myRigidbody2D;

    private void Awake()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        dashSpeed = movementSpeed * dashSpeedCoef;
    }
    private void FixedUpdate()
    {
        DoDashing();
    }
    public void Move(Vector2 rawInput)
    {
        //Vector3 delta = (rawInput * movementSpeed * Time.deltaTime);
        //transform.position += delta;
        //_myRigidbody2D.linearVelocity = delta;
        _myRigidbody2D.MovePosition(_myRigidbody2D.position + rawInput * movementSpeed * Time.fixedDeltaTime);
        currentUnitVisual.Animate(Time.deltaTime);
    }
    public void Dash()
    {
        if (isDashing) 
        {
            return;
        }
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosVector = mousePos;//remove eventually
        float playerToMouseDistance = Vector2.Distance(this.transform.position, mousePos);
        Debug.Log("playerToMouse distance is "+playerToMouseDistance);
        if (maxDashDistance < playerToMouseDistance)
        {
            _dashDestination = ReturnDashPosition(mousePos);
        }
        else
        {
            _dashDestination = mousePos;
        }
        isDashing = true;

        //take direction from mouse pos, if distance to mouse is greater than max dash distance
        //move to max distance, else move to mouse pos
        //Start dash cd
    }
    private Vector3 ReturnDashPosition(Vector3 mousePos)
    {
        Vector3 destination;
        float angle = (_mouseFollow.transform.localEulerAngles.z+90)*Mathf.Deg2Rad;//90 bcs its rotated
        Debug.Log("getting point at angle (rad) "+angle);
        float sinAngle = Mathf.Sin(angle);
        Debug.Log("sin of angle is " + sinAngle);
        float xIncrement = Mathf.Cos(angle) * maxDashDistance;
        float yIncrement = Mathf.Sin(angle) * maxDashDistance;
        destination = new Vector3(transform.position.x+xIncrement, transform.position.y+yIncrement, transform.position.z);
        return destination;
    }
    public void DoDashing()
    {
        if (isDashing)
        {
            //doing movement if 
            Vector3 delta = (_dashDestination * dashSpeed * Time.deltaTime);
            _myRigidbody2D.linearVelocity = delta;
            //_myRigidbody2D.MovePosition(_myRigidbody2D.position + (Vector2)_dashDestination * movementSpeed * Time.fixedDeltaTime);
            //_myRigidbody2D.MovePosition(_dashDestination * dashSpeed * Time.fixedDeltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _dashDestination, dashSpeed*Time.deltaTime);
            float distance = Vector3.Distance(transform.position, _dashDestination);
            if (distance < 0.01)
            {
                isDashing = false;
            }
        }
    }
}
