using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class SpotlightChangingColors : MonoBehaviour
{
    [SerializeField] private SpotlightFollowing spotlight;
    [SerializeField] List<Color> colors = new List<Color> { Color.red, Color.blue, Color.green };
    [SerializeField] float colorDuration = 5.0f;

    public bool isChangingColors
    {
        get
        {
            return _routine != null;
        }
        set
        {
            if (value)
            {
                _routine = StartCoroutine(ChangeColor());
            }
            else if (_routine != null)
            {
                StopCoroutine(_routine);
            }
        }
    }
    public int currentColorIndex;

    private Coroutine _routine;

    void Start()
    {
        currentColorIndex = 0;
        spotlight.lightColor = colors[currentColorIndex];

        isChangingColors = true;
    }

    private IEnumerator ChangeColor()
    {
        int lastColorIndex = colors.Count - 1;
        while (true)
        {
            yield return new WaitForSeconds(colorDuration);
            currentColorIndex = currentColorIndex >= lastColorIndex ? 0 : currentColorIndex + 1;
            spotlight.lightColor = colors[currentColorIndex];
        }
    }
}
