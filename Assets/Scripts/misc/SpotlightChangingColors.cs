using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class SpotlightChangingColors : MonoBehaviour
{
    [SerializeField] private SpotlightFollowing spotlight;
    [SerializeField] private float colorDuration = 5.0f;
    [SerializeField] private List<Color> colors = new List<Color> { Color.red, Color.green, Color.blue };

    public Action colorHasChanged;
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
    public Color currentColor
    {
        get { return colors[_currentColorIndex]; }
    }

    private int _currentColorIndex = 0;
    private Coroutine _routine;

    void Start()
    {
        _currentColorIndex = 0;
        spotlight.lightColor = colors[_currentColorIndex];

        isChangingColors = true;
    }

    public Type CurrentColorType()
    {
        return (Type)_currentColorIndex;
    }

    private IEnumerator ChangeColor()
    {
        int lastColorIndex = colors.Count - 1;
        while (true)
        {
            yield return new WaitForSeconds(colorDuration);
            _currentColorIndex = _currentColorIndex >= lastColorIndex ? 0 : _currentColorIndex + 1;
            spotlight.lightColor = colors[_currentColorIndex];

            colorHasChanged?.Invoke();
        }
    }
}
