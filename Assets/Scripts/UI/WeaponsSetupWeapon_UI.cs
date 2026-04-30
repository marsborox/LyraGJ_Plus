using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSetupWeapon_UI : UI
{
    public Image weaponImage;
    public string weaponName;
    public Button setColorRedButton;
    public Button setColorGreenButton;
    public Button setColorBlueButton;
    public Button setColorWhiteButton;
    
    public Color32 red = new Color32(255,0,0,255);
    public Color32 green = new Color32(0,255,0,255);
    public Color32 blue = new Color32(0,0,255,255);
    public Color32 white = new Color32(255,255,255,255);

    private void Start()
    {
        InitiateButton(setColorRedButton, SetWeaponRed);
        InitiateButton(setColorGreenButton, SetWeaponGreen);
        InitiateButton(setColorBlueButton,SetWeaponBlue);
        InitiateButton(setColorWhiteButton,SetWeaponWhite);

    }

    public void SetupButtons(Action<Type> setWeapon)
    {//should be proper
        InitiateButton(setColorRedButton, setWeapon,Type.RED,SetWeaponPictogramRed);
        InitiateButton(setColorGreenButton, setWeapon,Type.GREEN,SetWeaponPictogramGreen);
        InitiateButton(setColorBlueButton, setWeapon,Type.BLUE,SetWeaponPictogramBlue);
        InitiateButton(setColorWhiteButton, setWeapon,Type.WHITE,SetWeaponPictogramWhite);
    }
    
    /*private void SetWeaponField(Weapon inputWeapon)
    { 
        //weapon = inputWeapon;
    }*/

    public void SetWeaponPictogramRed()
    { 
        weaponImage.color = red;
    }
    public void SetWeaponPictogramGreen()
    { 
        weaponImage.color = green;
    }
    public void SetWeaponPictogramBlue() 
    {
        weaponImage.color = blue;
    }
    public void SetWeaponPictogramWhite()
    {
        weaponImage.color = white;
    }

    //this will set weapons
    //delete this
    public void SetWeaponRed()
    { 
        //PlayerWeaponTracker.instance.SetCorrectWeaponType(weapon,Type.RED);        
        //weaponImage.color = red;
    }
    public void SetWeaponGreen()
    {
        //PlayerWeaponTracker.instance.SetCorrectWeaponType(weapon,Type.GREEN);        
        //weaponImage.color = green;
    }
    public void SetWeaponBlue() 
    {
        //PlayerWeaponTracker.instance.SetCorrectWeaponType(weapon,Type.BLUE);
        //weaponImage.color = blue;
    }
    public void SetWeaponWhite()
    {
        //PlayerWeaponTracker.instance.SetCorrectWeaponType(weapon,Type.WHITE);
        //weaponImage.color = white;
    }
    /*//delete this
    public void SetWeaponRed(Action<Type> method)
    { 
        method(Type.RED);
    }
    public void SetWeaponGreen(Action<Type> method)
    {
        method(Type.GREEN);
    }
    public void SetWeaponBlue(Action<Type> method) 
    {
        method(Type.BLUE);
    }
    public void SetWeaponWhite(Action<Type> method)
    {
        method(Type.WHITE);
    }*/
}
