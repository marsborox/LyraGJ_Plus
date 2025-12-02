using System;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.Rendering.DebugUI;
public class PlayerController : MonoBehaviour
{
    public Player player;
    public PlayerMovement playerMovement;
    [SerializeField] private MouseFollow _mouseFollow;
    [SerializeField] private AnimationController _animationController;
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


    private Clicker _LMBClicker = new Clicker();
    private Clicker _RMBClicker = new Clicker();
    private Clicker _action3Clicker = new Clicker();
    private Clicker _action4Clicker = new Clicker();

    private PlayerInput _playerInput;
    private InputAction _LMB1;
    private InputAction _RMB2;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        //_action1 = _playerInput.actions["Action1"];
        //_action2 = _playerInput.actions["Action2"];
        _LMBClicker.action = _playerInput.actions["Action1"];
        _LMBClicker.name = "action1";
        _RMBClicker.action = _playerInput.actions["Action2"];
        _RMBClicker.name = "action2";
    }
    void Start()
    {

    }
    private void OnEnable()
    {
        _LMBClicker.action.started += ctx => StartStopPressed(ref _LMBClicker/*,player.AttackWeapon1Click*/);
        _LMBClicker.action.canceled += ctx => StartStopPressed(ref _LMBClicker /*,player.AttackWeapon1Click*/);

        _RMBClicker.action.started += ctx => StartStopPressed(ref _RMBClicker /*,player.AttackWeapon2Click*/);
        _RMBClicker.action.canceled += ctx => StartStopPressed(ref _RMBClicker /*,player.AttackWeapon2Click*/);
        //_action1.started += ctx => StartPressed1();
        //_action1.canceled += ctx => StopPressed1();
        //_action2.started += ctx => StartPressed2();
        //_action2.canceled += ctx => StopPressed2();
    }
    private void OnDisable()
    {

        _LMBClicker.action.started -= ctx => StartStopPressed(ref _LMBClicker);
        _LMBClicker.action.canceled -= ctx => StartStopPressed(ref _LMBClicker);

        _RMBClicker.action.started -= ctx => StartStopPressed(ref _RMBClicker);
        _RMBClicker.action.canceled -= ctx => StartStopPressed(ref _RMBClicker);

        //_action1.started -= ctx => StartPressed1();
        //_action1.canceled -= ctx => StopPressed1();
        //_action2.started -= ctx => StartPressed2();
        //_action2.canceled -= ctx => StopPressed2();
    }

    void FixedUpdate()
    {
        player.playerMovement.MoveByVector(_rawInput);// from HadesControls
        //player.input = _rawInput;
        //CheckClickHoldAction();
        CheckClickHoldActions();
    }
    void Update()
    {
        
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
        //player.playerMovement.DashWSAD(_rawInput);
        player.playerMovement.DashMouse();
        
    }
    #region 1234attacks
    void OnWeapon1()
    {
        //Debug.Log("weapon1");
        //player.AttackWeapon1();
        player.AttackWeapon1Click();
        _animationController.HandleAnimation();
    }
    void OnWeapon2()
    {
        //Debug.Log("weapon2");
        //player.AttackWeapon2();
        player.AttackWeapon2Click();
        _animationController.HandleAnimation();
    }
    void OnWeapon3()
    {
        //Debug.Log("weapon3");
        //player.AttackWeapon3();
        player.AttackWeapon3Click();
        _animationController.HandleAnimation();
    }
    void OnWeapon4()
    {
        //Debug.Log("weapon4");
        player.AttackWeapon4Click();
        _animationController.HandleAnimation();
    }
    #endregion
    void OnAction1()
    { 
        
    }
    void OnAction2()
    { 
        
    }
    void OnAction3()
    {//q
        //Debug.Log("action3");
        player.AttackWeapon3Click();
    }
    void OnAction4()
    {//e
        player.AttackWeapon4Click();
    }
    void CheckClickHoldActions()
    {
        //CheckClickHoldAction(ref _action1Clicker);
        //CheckClickHoldAction(ref _action2Clicker);

        //CheckClickHoldAction(ref _LMBClicker, player.AttackWeapon1Hold);
        //CheckClickHoldAction(ref _LMB2Clicker, player.AttackWeapon2Hold);

        CheckClickHoldAction(ref _LMBClicker, player.MoveLMB);

        //CheckClickHoldAction(ref _LMB2Clicker, player.AttackWeapon2Hold);
    }

    void StartStopPressed(ref Clicker clicker/*,Action onClick, Action onHold*/)
    {// thi is just to take click method kidna discontinued
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
                //Debug.Log(clicker.name + " was held for (s): " + clicker.actionTimer.ToString());
            }
            else
            {//do click thing
                //Debug.Log(clicker.name + " was clicked");
            }
            clicker.isPressed = false;
            clicker.isHeld = false;
            clicker.actionTimer = 0;
        }
    }
    void StartStopPressed(ref Clicker clicker,Action onClick,Action onHold)
    {
        if (!clicker.isPressed)
        {   //to detect just one click
            clicker.isPressed = true;
            onClick();
        }
        else
        {   //on stop
            //press released
            if (clicker.isHeld)
            {
                onHold();
                //DoHeldThing
                //Debug.Log(clicker.name + " was held for (s): " + clicker.actionTimer.ToString());
            }
            else
            {//do click thing
                //Debug.Log(clicker.name + " was clicked");
            }
            clicker.isPressed = false;
            clicker.isHeld = false;
            clicker.actionTimer = 0;
        }
    }
    void CheckClickHoldAction(ref Clicker clicker)
    {
        if (clicker.isPressed)
        {
            //clicker.isPressed = true;
            clicker.actionTimer += Time.deltaTime;
            if (clicker.actionTimer > holdHreshold)
            {
                clicker.isHeld = true;
            }
        }
    }
    void CheckClickHoldAction(ref Clicker clicker, Action onHold)
    {
        if (clicker.isPressed)
        {
            //onClick();
            //clicker.isPressed = true;
            clicker.actionTimer += Time.deltaTime;
            if (clicker.actionTimer > holdHreshold)
            {
                onHold();
                clicker.isHeld = true;
            }
        }
    }
    void DoNothing()
    { }
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