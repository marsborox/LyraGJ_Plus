using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponTracker : SingletonPersistent<PlayerWeaponTracker>
{
    public static new PlayerWeaponTracker instance => SingletonPersistent<PlayerWeaponTracker>.instance;

    public Weapon weaponGuitar
    {
        get {return ((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponGuitar;}//errory
        set {((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponGuitar = value;}
    }
    public Weapon weaponPiano
    {
        get{return ((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponPiano;}
        set {((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponPiano=value;}
    }
    public Weapon weaponSax
    {
        get{return ((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponSax;}
        set {((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponSax=value;}
    }
    public Weapon weaponGrenadeLauncher
    {
        get{return ((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponGrenadeLauncher;}
        set {((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponGrenadeLauncher=value;}
    }
    public Type guitarType = Type.RED;
    public Type pianoType = Type.GREEN;
    public Type saxType = Type.BLUE;
    public Type grenadeLauncherType = Type.WHITE;

    public WeaponsSetupSave weaponsSetup;
    Dictionary<Weapon, Type> weaponTypeDictionary = new Dictionary<Weapon, Type>();
    Player player {get{return GameManager.instance.ReturnPlayer();}}//errory
    protected override void Awake()
    {
        base.Awake();
        AddPairsToDictionary();
    }

    public void SetPlayerWeapons()
    {        
        weaponGuitar.SetWeaponType(guitarType);
        weaponPiano.SetWeaponType(pianoType);
        weaponSax.SetWeaponType(saxType);
        weaponGrenadeLauncher.SetWeaponType(grenadeLauncherType);
    }
    public void ReturnPlayerWeapons()
    {
        guitarType = weaponGuitar.ReturnWeaponType();
        pianoType = weaponPiano.ReturnWeaponType();
        saxType = weaponSax.ReturnWeaponType();
        grenadeLauncherType = weaponGrenadeLauncher.ReturnWeaponType();
    }
    public void SetCorrectWeaponType(Weapon weapon,Type type)
    {
        weaponTypeDictionary[weapon] = type;
    }
    void ReturnCorrectWeaponType(Weapon weapon,Type type)
    {//delete
        Type result = Type.DEFAULT;
        weaponTypeDictionary.TryGetValue(weapon,out result);
        result = type;
    }
    private void AddPairsToDictionary()
    {
        weaponTypeDictionary.Add(weaponGuitar,guitarType);
        weaponTypeDictionary.Add(weaponPiano,pianoType);
        weaponTypeDictionary.Add(weaponSax,saxType);
        weaponTypeDictionary.Add(weaponGrenadeLauncher,grenadeLauncherType);
    }
    public void TestConnectivity()
    {
        Debug.Log("WeaponTracker visible");
    }
    public void SetGuitarType(Type weaponType)
    {
        guitarType = weaponType;
    }
    public void SetPianoType(Type weaponType)
    {
        pianoType = weaponType;
    }
    public void SetSaxType(Type weaponType)
    {
        saxType = weaponType;
    }
    public void SetGrenadeLauncherType(Type weaponType)
    {
        grenadeLauncherType = weaponType;
    }
    public void LoadWeaponSetup()
    {

        ((PlayerCombat)player.unitCombat).SetAllWeapons(guitarType,pianoType,saxType,grenadeLauncherType);
    }
    public void SetDefaultWeaponTypes()
    {
        guitarType = Type.RED;
        pianoType = Type.GREEN;
        saxType = Type.BLUE;
        grenadeLauncherType = Type.WHITE;
    }
}
