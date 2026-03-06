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

    private void Start()
    {
        InitiateButton(setColorRedButton, SetWeaponRed);
        InitiateButton(setColorGreenButton, SetWeaponGreen);
        InitiateButton(setColorBlueButton,SetWeaponBlue);
    }
    public void SetWeaponField(Weapon weapon)
    { 
        
    }
    void SetWeaponRed()
    { 
        weapon.SetWeaponRed();
    }
    void SetWeaponGreen()
    { 
        weapon.SetWeaponGreen();
    }
    void SetWeaponBlue() 
    {
        weapon.SetWeaponBlue();
    }

}
