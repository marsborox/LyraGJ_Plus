using System;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public Player player;
    public float holdHreshold=0.2f;//100/250ms 50frames/1s
    private Vector2 _rawInput;

    public class Clicker
    {
        public string name;
        public InputAction action;
        public bool isPressed = false;
        public bool isHeld = false;
        public float actionTimer = 0f;
    }

    private Clicker action1Clicker = new Clicker();
    private Clicker action2Clicker = new Clicker();
    private Clicker action3Clicker = new Clicker();
    private Clicker action4Clicker = new Clicker();

    private PlayerInput _playerInput;
    private InputAction _action1;
    private InputAction _action2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        //_action1 = _playerInput.actions["Action1"];
        //_action2 = _playerInput.actions["Action2"];
        action1Clicker.action = _playerInput.actions["Action1"];
        action1Clicker.name = "action1";
        action2Clicker.action = _playerInput.actions["Action2"];
        action2Clicker.name = "action2";
    }
    void Start()
    {

    }
    private void OnEnable()
    {
        action1Clicker.action.started += ctx => StartStopPressed(ref action1Clicker);
        action1Clicker.action.canceled += ctx => StartStopPressed(ref action1Clicker);

        action2Clicker.action.started += ctx => StartStopPressed(ref action2Clicker);
        action2Clicker.action.canceled += ctx => StartStopPressed(ref action2Clicker);
        //_action1.started += ctx => StartPressed1();
        //_action1.canceled += ctx => StopPressed1();
        //_action2.started += ctx => StartPressed2();
        //_action2.canceled += ctx => StopPressed2();
    }
    private void OnDisable()
    {

        action1Clicker.action.started -= ctx => StartStopPressed(ref action1Clicker);
        action1Clicker.action.canceled -= ctx => StartStopPressed(ref action1Clicker);

        action2Clicker.action.started -= ctx => StartStopPressed(ref action2Clicker);
        action2Clicker.action.canceled -= ctx => StartStopPressed(ref action2Clicker);

        //_action1.started -= ctx => StartPressed1();
        //_action1.canceled -= ctx => StopPressed1();
        //_action2.started -= ctx => StartPressed2();
        //_action2.canceled -= ctx => StopPressed2();
    }
    private void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        player.playerMovement.Move(_rawInput);
        player.input = _rawInput;
        //CheckClickHoldAction();
        CheckClickHoldActions();
    }
    void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
        //Debug.Log(_rawInput);
        if (_rawInput.x == -1)
        {
            //player.isMovingLeft = true;
            player.playerMovement.currentDirection = PlayerMovement.Direction.LEFT;
        }
        else if (_rawInput.x == 1)
        {
            player.playerMovement.currentDirection = PlayerMovement.Direction.RIGHT;
            //player.isMovingLeft = false;
        }
        else if (_rawInput.y == -1)
        {
            player.playerMovement.currentDirection = PlayerMovement.Direction.DOWN;
        }
        else if (_rawInput.y == 1)
        {
            player.playerMovement.currentDirection = PlayerMovement.Direction.UP;
        }
    }
    void OnDash()
    { 
        
    }
    void OnWeapon1()
    {
        //Debug.Log("weapon1");
        player.AttackWeapon1();
    }
    void OnWeapon2()
    {
        //Debug.Log("weapon2");
        player.AttackWeapon2();
    }
    void OnWeapon3()
    {
        //Debug.Log("weapon3");
        player.AttackWeapon3();
    }
    void OnWeapon4()
    {
        //Debug.Log("weapon4");
        //player.AttackWeapon3();
    }
    void OnAction1()
    { 
    
    }
    void OnAction2()
    { 
    
    }
    void StartStopPressed(ref Clicker clicker)
    {
        if(!clicker.isPressed)
        {   //onstart stop
            clicker.isPressed = true;
        }
        else
        {   //on stop
            //press released
            if (clicker.isHeld)
            {
                //DoHeldThing
                Debug.Log(clicker.name + " was held for (s): " + clicker.actionTimer.ToString());
            }
            else
            {//do click thing
                Debug.Log(clicker.name + " was clicked");
            }
            clicker.isPressed = false;
            clicker.isHeld = false;
            clicker.actionTimer = 0;
        }
    }
    void CheckClickHoldActions()
    { 
        CheckClickHoldAction(ref action1Clicker);
        CheckClickHoldAction(ref action2Clicker);
    }
    void CheckClickHoldAction(ref Clicker clicker)
    {
        if (clicker.isPressed)
        {
            clicker.isPressed = true;
            clicker.actionTimer += Time.deltaTime;
            if (clicker.actionTimer > holdHreshold)
            {
                clicker.isHeld = true;
            }

        }
    }
}

    #region discontinued
    /*private void StartPressed1()
    {
        _isPressed1 = true;
        //press released
    }*/
    /*private void StopPressed1()
    {
        //press released
        if (_isHeld1)
        {
            //DoHeldThing
            Debug.Log("Mouse1 was held for (s): "+action1timer.ToString());

        }
        else
        {
            Debug.Log("Mouse1 was clicked");
        }
        _isPressed1 = false;
        _isHeld1 = false;
        action1timer=0;
    }*/
    /*private void StartPressed2()
    {
        _isPressed2 = true;

    }*/
    /*private void StopPressed2()
    {
        _isPressed2 = false;
    }*/
    /*void CheckClickHoldAction()
    {
        if (_isPressed1) 
        {
            _isPressed1 = true;
            action1timer += Time.deltaTime;
            if (action1timer > holdHreshold)
            {
                _isHeld1 = true;
            }
            
        }
    }*/
    #endregion