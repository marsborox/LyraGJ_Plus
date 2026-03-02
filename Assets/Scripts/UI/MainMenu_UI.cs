using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI : UI
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private UI _optionsUI;

    [SerializeField] private UI _credits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // InitiateButton(_newGameButton, );
        InitiateButton(_quitButton, QuitToWindows);
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
