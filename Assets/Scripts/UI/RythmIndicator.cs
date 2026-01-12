using UnityEngine;
using UnityEngine.UI;

public class RythmIndicator : MonoBehaviour
{
    public RythmBonus playerRythmBonus;
    [SerializeField] private GameObject rythmBubble;
    [SerializeField] private Image rythmBubbleImage;
    [SerializeField] Color emphasisColor;
    [SerializeField] Color otherColor;

    private void Update()
    {
        CheckBubblePosition();
        SetColor();
    }
    private void CheckBubblePosition()
    {
        float angle = playerRythmBonus.rythmBonusCheckValue / playerRythmBonus.period*360;
        rythmBubble.transform.rotation = Quaternion.Euler(0,0,angle);
        //Debug.Log("rythm bubble angle: "+angle);
    }
    private void SetColor()
    {
        if (playerRythmBonus.CheckIfInRythm())
        { 
            rythmBubbleImage.color = emphasisColor;
        }
        else
        {
            rythmBubbleImage.color = otherColor;
        }
    }
}
