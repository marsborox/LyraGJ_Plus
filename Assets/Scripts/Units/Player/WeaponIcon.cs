using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    public Image icon;
    public Weapon weaponToTrack;

    void Update()
    {
        icon.fillAmount = weaponToTrack.CoolDownValue();
    }
}
