using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseUI : MenuUI
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button lobbyButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button closeButton;

    void OnEnable()
    {
        if (resumeButton != null) {
            resumeButton.onClick.AddListener(OnResumeButtonClick);
        }
        if (optionsButton != null) {
            optionsButton.onClick.AddListener(OnOptionsButtonClick);
        }
        if (controlsButton != null) {
            controlsButton.onClick.AddListener(OnControlsButtonClick);
        }
        if (lobbyButton != null) {
            lobbyButton.onClick.AddListener(OnLobbyButtonClick);
        }
        if (quitGameButton != null) {
            quitGameButton.onClick.AddListener(OnQuitGameButtonClick);
        }
        if (closeButton != null) {
            closeButton.onClick.AddListener(OnCloseButtonClick);
        }
    }
    void OnDisable()
    {
        if (resumeButton != null) {
            resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        }
        if (optionsButton != null) {
            optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
        }
        if (controlsButton != null) {
            controlsButton.onClick.RemoveListener(OnControlsButtonClick);
        }
        if (lobbyButton != null) {
            lobbyButton.onClick.RemoveListener(OnLobbyButtonClick);
        }
        if (quitGameButton != null) {
            quitGameButton.onClick.RemoveListener(OnQuitGameButtonClick);
        }
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }
    }
    private void OnResumeButtonClick()
    {
        CloseMenu();
    }
    private void OnOptionsButtonClick()
    {
        MySceneManager.instance.OpenOptions();
    }
    private void OnControlsButtonClick()
    {
        // TODO
    }
    private void OnLobbyButtonClick()
    {
        MySceneManager.instance.OpenLobby();
    }
    private void OnQuitGameButtonClick()
    {
        MySceneManager.instance.OpenScene(GameScene.MAIN_MENU);
    }
    private void OnCloseButtonClick()
    {
        CloseMenu();
    }
}
