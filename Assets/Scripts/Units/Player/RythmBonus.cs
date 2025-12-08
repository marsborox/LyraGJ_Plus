using UnityEngine;

public class RythmBonus : MonoBehaviour
{
    public float rythmBonusCheckValue=0;
    public float bpm = 170;
    public float beatPeriod = 4;//once per how many beats
    public float period;
    public float accuracyTolerance = 0.1f;
    private void Start()
    {
        SetFrequency();
    }
    private void Update()
    {
        RythmBonusTimer();
    }
    public bool CheckIfInRythm()
    { 
        float accuracyModifier = period * accuracyTolerance;
        if (rythmBonusCheckValue < accuracyModifier || rythmBonusCheckValue > (period - accuracyModifier))
        {
            //Debug.Log("In rythm");
            return true;
        }
        else 
        {
            //Debug.Log("notInRythm");
            return false; 
        }
    }

    private void RythmBonusTimer()
    {
        rythmBonusCheckValue += ( Time.deltaTime/*/beatFrequency*/);
        if (rythmBonusCheckValue > period)
        {
            rythmBonusCheckValue -= period;
        }
    }
    private void SetFrequency()
    {
        float frequency = bpm / 60;
        period = beatPeriod / frequency;
    }
}