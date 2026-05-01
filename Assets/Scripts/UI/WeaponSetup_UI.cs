using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsSetup_UI : UI
{
    [SerializeField] private WeaponSetupWeapon_UI _weaponGuitarUI;
    [SerializeField] private WeaponSetupWeapon_UI _weaponPianoUI;
    [SerializeField] private WeaponSetupWeapon_UI _weaponSaxUI;
    [SerializeField] private WeaponSetupWeapon_UI _grenadeLanucherUI;

    [SerializeField] private Button _exitButton;
    public void Awake()
    {

    }
    void Start()
    {
        SetPictogramsColor();
        SetupWeaponPanels();
    }

    public void SetPictogramsColor()
    {
        SetPictogramColor(_weaponGuitarUI,PlayerWeaponTracker.instance.guitarType);
        SetPictogramColor(_weaponPianoUI,PlayerWeaponTracker.instance.pianoType);
        SetPictogramColor(_weaponSaxUI,PlayerWeaponTracker.instance.saxType);
        SetPictogramColor(_grenadeLanucherUI,PlayerWeaponTracker.instance.grenadeLauncherType);
    }
    public void SetupWeaponPanels()
    {//subscribe buttons to change picked weapon
        var weaponTracker = PlayerWeaponTracker.instance;
        _weaponGuitarUI.SetupButtons(weaponTracker.SetGuitarType);
        _weaponPianoUI.SetupButtons(weaponTracker.SetPianoType);
        _weaponSaxUI.SetupButtons(weaponTracker.SetSaxType);
        _grenadeLanucherUI.SetupButtons(weaponTracker.SetGrenadeLauncherType);
    }
    private void SetPictogramColor(WeaponSetupWeapon_UI weaponSetupUI, Type weaponType)
    {
        switch(weaponType)
        {
            case Type.RED:
                {
                    weaponSetupUI.SetWeaponPictogramRed();
                    break;
                }
            case Type.GREEN:
                {
                    weaponSetupUI.SetWeaponPictogramGreen();
                    break;
                }
            case Type.BLUE:
                {
                    weaponSetupUI.SetWeaponPictogramBlue();
                    break;
                }   
            case Type.WHITE:
                {
                    weaponSetupUI.SetWeaponPictogramWhite();
                    break;
                }
            default:
                {
                    Debug.Log("Type not implemented");
                    break;
                }
        }
    }
}
