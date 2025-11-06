using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Unit
{
    
    public Vector2 input;
    public bool isMovingLeft=true;
    [SerializeField] private int _health=10;

    public Weapon weapon1;
    public Weapon weapon2;
    public Weapon weapon3;
    public Weapon weapon4;

    public PlayerMovement playerMovement;
    //private Rigidbody2D _myRigidbody2D;
    private void Awake()
    {
        //_myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        base.Update();
        //FaceCorrectDirection();
    }

    public void AttackWeapon1()
    {/*
        if (weapon1.CanAttack())
        {
            MySoundManager.instance.PlayGuitar();
            weapon1.Attack();
        }*/
    }
    public void AttackWeapon2()
    {
        /*if (weapon2.CanAttack())
        {
            MySoundManager.instance.PlayPiano();
            weapon2.Attack();
        }*/
    }
    public void AttackWeapon3()
    {
        /*if (weapon3.CanAttack())
        {
            MySoundManager.instance.PlaySaxofone();
            weapon3.Attack();
        }*/
    }
    public void AttackWeapon4()
    {
        /*if (weapon4.CanAttack())
        {
            //MySoundManager.instance.PlaySaxofone();
            weapon4.Attack();
        }*/
    }

}
