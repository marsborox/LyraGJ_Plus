using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator_UI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Player _player;
    [SerializeField]public float _minHealthImageFill=0.11f;
    [SerializeField]public float _maxHealthImageFill=0.84f;
    public void Update()
    {
        //HealthBarFill();
        CalcFillAmountRelative();
    }

    void CalcFillAmountRelative()
    {
        float realRange = _maxHealthImageFill - _minHealthImageFill;
        //trojclenka relative  fill fraction as whole isnt 1

        float relativeFraction = (float)_player.ReturnHealthCurrent() / (float)_player.ReturnHealthMax();
        //add min value
        float realFraction = _minHealthImageFill + (relativeFraction*realRange);
        //Debug.Log("range= "+realRange+" fraction= "+realFraction);
        _healthBar.fillAmount = realFraction;
    }
    void HealthBarFill()
    {


        float healthFraction = (float)_player.ReturnHealthCurrent() / (float)_player.ReturnHealthMax();
        _healthBar.fillAmount = healthFraction;
        //Debug.Log("fillingHealthInUI");
    }
}
