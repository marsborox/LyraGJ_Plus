using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Unit
{
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
    {
        ((PlayerCombat)unitCombat).AttackWeapon1();
    }
    public void AttackWeapon2()
    {
        ((PlayerCombat)unitCombat).AttackWeapon2();
    }
    public void AttackWeapon3()
    {
        ((PlayerCombat)unitCombat).AttackWeapon3();
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
