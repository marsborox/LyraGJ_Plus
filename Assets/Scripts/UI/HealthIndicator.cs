using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator_UI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Player _player;
    public void Update()
    {
        HealthBarFill();
    }

    void HealthBarFill()
    {
        float healthFraction = (float)_player.ReturnHealthCurrent() / (float)_player.ReturnHealthMax();
        _healthBar.fillAmount = healthFraction;
        //Debug.Log("fillingHealthInUI");
    }
}
