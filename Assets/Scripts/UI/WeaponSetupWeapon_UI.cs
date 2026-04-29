using UnityEngine;
using UnityEngine.UI;

public class WeaponSetupWeapon_UI : UI
{
    public Image weaponImage;
    public string weaponName;
    public Button setColorRedButton;
    public Button setColorGreenButton;
    public Button setColorBlueButton;
    public Weapon weapon;
    public Image weaponIndicator;

    public Color32 red = new Color32(255,0,0,217);
    public Color32 green = new Color32(0,255,0,217);
    public Color32 blue = new Color32(0,0,255,217);

    private void Start()
    {
        InitiateButton(setColorRedButton, SetWeaponRed);
        InitiateButton(setColorGreenButton, SetWeaponGreen);
        InitiateButton(setColorBlueButton,SetWeaponBlue);
    }
    
    public void SetWeaponField(Weapon inputWeapon)
    { 
        weapon = inputWeapon;
    }
    void SetWeaponRed()
    { 
        //fix
        //weapon.SetWeaponRed();
        weapon.SetWeaponType(Type.RED);

        weaponImage.color = red;
    }
    void SetWeaponGreen()
    { 
        //fix
        //weapon.SetWeaponGreen();
        weapon.SetWeaponType(Type.GREEN);
        weaponImage.color = green;
    }
    void SetWeaponBlue() 
    {
        //fix
        //weapon.SetWeaponBlue();
        weapon.SetWeaponType(Type.BLUE);
        weaponImage.color = blue;
    }

}
