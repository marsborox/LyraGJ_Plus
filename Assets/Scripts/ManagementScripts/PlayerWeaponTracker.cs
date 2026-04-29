using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponTracker : SingletonPersistent<PlayerWeaponTracker>
{
    public static new PlayerWeaponTracker instance => SingletonPersistent<PlayerWeaponTracker>.instance;

    public Weapon weaponGuitar{get{return ((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponGuitar;}}
    public Weapon weaponPiano{get{return ((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponPiano;}}
    public Weapon weaponSax{get{return ((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponSax;}}
    public Weapon weaponGrenadeLauncher{get{return ((PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat).weaponGrenadeLauncher;}}
    public Type guitarType = Type.RED;
    public Type pianoType = Type.GREEN;
    public Type saxType = Type.BLUE;
    public Type grenadeLauncherType = Type.WHITE;

    Dictionary<Weapon, Type> weaponTypeDictionary = new Dictionary<Weapon, Type>();
    Player player {get{return GameManager.instance.ReturnPlayer();}}
    protected override void Awake()
    {
        base.Awake();
    }  


    public void SetPlayerWeapons()
    {
        
        weaponGuitar.SetWeaponType(guitarType);
        weaponPiano.SetWeaponType(pianoType);
        weaponSax.SetWeaponType(saxType);
        weaponGrenadeLauncher.SetWeaponType(grenadeLauncherType);
    }
    public void SavePlayerWeapons()
    {

        guitarType = weaponGuitar.ReturnWeaponType();
        pianoType = weaponPiano.ReturnWeaponType();
        saxType = weaponSax.ReturnWeaponType();
        grenadeLauncherType = weaponGrenadeLauncher.ReturnWeaponType();
    }
    private void SavePlayerWeapon(Weapon weapon, Type weaponType)
    {
        weapon.SetWeaponType(weaponType);
    }
    public void ReturnCorrectWeaponType()
    {
        
    }
}
