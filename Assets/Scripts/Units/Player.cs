using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Unit
{
    public PlayerMovement playerMovement;
    public RythmBonus rythmBonus;
    //private Rigidbody2D _myRigidbody2D;
    
    private void Awake()
    {

        //_myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (GameManager.instance != null)
        {
            spriteLibrary.spriteLibraryAsset = GameManager.instance.levelSettings.lyraVisual;
        }
    }
    private void Update()
    {
        base.Update();
        //FaceCorrectDirection();
    }
    public void AttackWeapon1Click()
    {
        if (unitCombat == null) return;

        ((PlayerCombat)unitCombat).Weapon1_OnClick();
    }
    public void AttackWeapon1Hold()
    {
        if (unitCombat == null) return;

        ((PlayerCombat)unitCombat).Weapon1_OnHold();
    }
    public void AttackWeapon2Click()
    {
        if (unitCombat == null) return;

        ((PlayerCombat)unitCombat).Weapon2_OnClick();
    }
    public void AttackWeapon2Hold()
    {
        if (unitCombat == null) return;

        ((PlayerCombat)unitCombat).Weapon2_OnHold();
    }
    public void AttackWeapon3Click()
    {
        if (unitCombat == null) return;

        ((PlayerCombat)unitCombat).Weapon3_OnClick();
    }
    public void AttackWeapon4Click()
    {
        if (unitCombat == null) return;

        ((PlayerCombat)unitCombat).Weapon4_OnClick();
    }
    public void MoveLMB()
    {
        playerMovement.MoveByMouse();
    }
    public void OpenWeaponMenu()
    { 
    
    }
    /*public void StopMakeIdle()
    { 
        playerMovement.
    }*/
}
