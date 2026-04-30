using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public void InitiateButton(Button button, Action method)
    {//Might be issue here
        button.onClick.AddListener(delegate
        {
            method();
        });
        //boolUI = false;
    }
    public void InitiateButton<T>(Button button, Action<T> method,T value)
    {
        button.onClick.AddListener(delegate
        {
            method(value);
        });
        //boolUI = false;
    }
    public void InitiateButton<T>(Button button, Action<T> method,T value, Action method2)
    {
        button.onClick.AddListener(delegate
        {
            method(value);
            method2();
        });
        //boolUI = false;
    }
    public void ButtonMethod(Button button, GameObject gUIPanel)
    {//turn off/on uiMenuPanel
        if (!gUIPanel.activeSelf)
        {

            //tempBoolean = true;
            //button.GetComponent<Image>().color = pressedColor;
            gUIPanel.SetActive(true);
        }
        else
        {
            //tempBoolean = false;
            //button.GetComponent<Image>().color = unpressedColor;
            gUIPanel.SetActive(false);
        }
    }
}
