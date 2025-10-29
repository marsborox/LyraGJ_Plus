using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI : UI
{
    public Button newGameButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button quitButton;
    public UI optionsUI;

    public UI credits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitiateButton(newGameButton, MySceneManager.instance.OpenGameScene);
        InitiateButton(quitButton, QuitToWindows);
        //InitiateButton(creditsButton, ButtonGUIMethod, credits.gameObject);
        //InitiateButton(optionsButton, ButtonGUIMethod, optionsUI.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void QuitToWindows()
    {
        Application.Quit();
    }
}
