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

    void Start()
    {
        SetWeaponReferencies();
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
