using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponTracker : SingletonPersistent<PlayerWeaponTracker>
{
    public static new PlayerWeaponTracker instance => SingletonPersistent<PlayerWeaponTracker>.instance;

    public PlayerCombat playerCombat
    {
        get{return (PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat;}
    }
    /*
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
    }*/
    public Type guitarType = Type.RED;
    public Type pianoType = Type.GREEN;
    public Type saxType = Type.BLUE;
    public Type grenadeLauncherType = Type.WHITE;

    public WeaponsSetupSave weaponsSetup;
    Dictionary<Weapon, Type> weaponTypeDictionary = new Dictionary<Weapon, Type>();
    //Player player {get{return GameManager.instance.ReturnPlayer();}}//errory//want to remove
    protected override void Awake()
    {
        base.Awake();
        AddPairsToDictionary();
    }

    /*private void SetPlayerWeapons()
    {  //not used?      
        playerCombat.weaponGuitar.SetWeaponType(guitarType);
        playerCombat.weaponPiano.SetWeaponType(pianoType);
        playerCombat.weaponSax.SetWeaponType(saxType);
        playerCombat.weaponGrenadeLauncher.SetWeaponType(grenadeLauncherType);
    }
    private void ReturnPlayerWeapons()
    {//not used?    
        guitarType = playerCombat.weaponGuitar.ReturnWeaponType();
        pianoType = playerCombat.weaponPiano.ReturnWeaponType();
        saxType = playerCombat.weaponSax.ReturnWeaponType();
        grenadeLauncherType = playerCombat.weaponGrenadeLauncher.ReturnWeaponType();
    }*/
    /*
    private void SetCorrectWeaponType(Weapon weapon,Type type)
    {//not used?    
        weaponTypeDictionary[weapon] = type;
    }
    void ReturnCorrectWeaponType(Weapon weapon,Type type)
    {//delete //not used?    
        Type result = Type.DEFAULT;
        weaponTypeDictionary.TryGetValue(weapon,out result);
        result = type;
    }*/
    private void AddPairsToDictionary()
    {
        weaponTypeDictionary.Add(playerCombat.weaponGuitar,guitarType);
        weaponTypeDictionary.Add(playerCombat.weaponPiano,pianoType);
        weaponTypeDictionary.Add(playerCombat.weaponSax,saxType);
        weaponTypeDictionary.Add(playerCombat.weaponGrenadeLauncher,grenadeLauncherType);
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
        playerCombat.SetAllWeapons(guitarType,pianoType,saxType,grenadeLauncherType);
    }
    public void SetDefaultWeaponTypes()
    {
        guitarType = Type.RED;
        pianoType = Type.GREEN;
        saxType = Type.BLUE;
        grenadeLauncherType = Type.WHITE;
    }
}
