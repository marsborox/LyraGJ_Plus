using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Image guitarIcon;
    public Image pianoIcon;
    public Image saxIcon;
    //public Image weapon4Icon;
    public Weapon weaponGuitar;
    public Weapon weaponPiano;
    public Weapon weaponSax;
    //public Weapon weapon4New;
    public Color32 red = new Color32(255,0,0,217);
    public Color32 green = new Color32(0,255,0,217);
    public Color32 blue = new Color32(0,0,255,217);
    public Color32 white = new Color32(255,255,255,217);
    void Start()
    {
        SetWeaponReferencies();
        SetPanelsColors();
    }
    void Update()
    {
        FillAllPanels();
    }
    private void SetWeaponReferencies()
    {
        PlayerCombat playerCombat = (PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat;
        weaponGuitar = playerCombat.weaponGuitar;
        weaponPiano = playerCombat.weaponPiano;
        weaponSax = playerCombat.weaponSax;
        
    }
    private void SetPanelsColors()
    {
        PlayerCombat playerCombat = (PlayerCombat)GameManager.instance.ReturnPlayer().unitCombat;

        guitarIcon.color = ColorFromType(PlayerWeaponTracker.instance.guitarType);
        pianoIcon.color = ColorFromType(PlayerWeaponTracker.instance.pianoType);
        saxIcon.color = ColorFromType(PlayerWeaponTracker.instance.saxType);

        /*guitarIcon.color = ColorFromType(playerCombat.weaponGuitar.ReturnWeaponType());
        pianoIcon.color = ColorFromType(playerCombat.weaponPiano.ReturnWeaponType());
        saxIcon.color = ColorFromType(playerCombat.weaponSax.ReturnWeaponType());*/
    }
    private Color32 ColorFromType(Type weaponType)
    {
        switch(weaponType)
        {
            case Type.RED: return red;
            case Type.GREEN: return green;
            case Type.BLUE: return blue;
            case Type.WHITE: return white;
            
            default: return new Color32(120,120,120,120);
        }
    }
    void FillAllPanels()
    {
        FillPanel(guitarIcon, weaponGuitar);
        FillPanel(pianoIcon, weaponPiano);
        FillPanel(saxIcon,weaponSax);

    }
    void FillPanel(Image icon, Weapon weapon)
    {
        icon.fillAmount = weapon.CoolDownValue();
    }
}
