using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PauseUI : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button lobbyButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button closeButton;

    [Header("Content Wrappers")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject alert;
    [SerializeField] private GameObject options;

    [Header("Alert")]
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    [Header("Options")]
    [SerializeField] private TextMeshProUGUI musicValue;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI soundsValue;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private Button optionsCloseButton;

    private Action<bool> _onResult;

    public void ToggleMenu()
    {
        if (gameObject.activeInHierarchy)
        {
            CloseMenu();
        } else
        {
            OpenMenu();
        }
    }

    public void CloseMenu()
    {
        // special case in MainMenu scene
        if (MySceneManager.PreviousScene == null)
        {
            HideAll();
            return;            
        }

        if (controls.activeInHierarchy)
        {
            ShowControls(false);
        }
        else if (alert.activeInHierarchy)            
        {
            HideAlert();
        }
        else if (options.activeInHierarchy)            
        {
            ShowOptions(false);
        }
        else {
            HideAll();
        }
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowOptions()
    {
        ShowOptions(true);
    }

    void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        optionsButton.onClick.AddListener(OnOptionsButtonClick);
        controlsButton.onClick.AddListener(OnControlsButtonClick);
        lobbyButton.onClick.AddListener(OnLobbyButtonClick);
        quitGameButton.onClick.AddListener(OnQuitGameButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);

        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);

        musicSlider.value = MySoundManager.instance.musicVolume;
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);

        soundsSlider.value = MySoundManager.instance.soundEffectsVolume;
        soundsSlider.onValueChanged.AddListener(OnSoundsSliderChanged);

        optionsCloseButton.onClick.AddListener(OnCloseButtonClick);

        CalculateValues();
    }
    void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
        controlsButton.onClick.RemoveListener(OnControlsButtonClick);
        lobbyButton.onClick.RemoveListener(OnLobbyButtonClick);
        quitGameButton.onClick.RemoveListener(OnQuitGameButtonClick);
        closeButton.onClick.RemoveListener(OnCloseButtonClick);

        confirmButton.onClick.RemoveListener(OnConfirmButtonClick);
        cancelButton.onClick.RemoveListener(OnCancelButtonClick);

        musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
        soundsSlider.onValueChanged.RemoveListener(OnSoundsSliderChanged);
        optionsCloseButton.onClick.RemoveListener(OnCloseButtonClick);

        CalculateValues();
    }
    private void OnResumeButtonClick()
    {
        CloseMenu();
    }
    private void OnOptionsButtonClick()
    {
        ShowOptions(true);
    }
    private void OnControlsButtonClick()
    {
        ShowControls(true);
    }
    private void OnLobbyButtonClick()
    {
        ShowAlert("Exit to Lobby? Your content will be lost.\nAre you sure?", confirmed => {
            if (confirmed) {
                HideAll();
                MySceneManager.instance.OpenLobby();
            }
        });
    }
    private void OnQuitGameButtonClick()
    {
        ShowAlert("Exit game? Your content will be lost.\nAre you sure?", confirmed => {
            if (confirmed) {
                HideAll();
                MySceneManager.instance.OpenScene(GameScene.MAIN_MENU);
            }
        });
    }
    private void OnCloseButtonClick()
    {
        CloseMenu();
    }

    // Options

    private void OnMusicSliderChanged(float value)
    {
        MySoundManager.instance.ChangeMusicVolume(value);
        CalculateValues();
    }
    private void OnSoundsSliderChanged(float value)
    {
        MySoundManager.instance.ChangeSoundEffectsVolume(value);
        CalculateValues();
    }

    private void CalculateValues()
    {
        float music = Mathf.RoundToInt(MySoundManager.instance.musicVolume * 100f);
        musicValue.text = music + "%";

        float sounds = Mathf.RoundToInt(MySoundManager.instance.soundEffectsVolume * 100f);
        soundsValue.text = sounds + "%";
    }

    // Handle contents

    private void ShowControls(bool show)
    {
        buttons.SetActive(!show);
        controls.SetActive(show);
    }
    private void ShowAlert(string alertText, Action<bool> onResult)
    {
        this.alertText.text = alertText;

        _onResult = onResult;
        
        buttons.SetActive(false);
        alert.SetActive(true);
    }
    private void HideAlert()
    {
        buttons.SetActive(true);
        alert.SetActive(false);        
    }

    private void ShowOptions(bool show)
    {
        options.SetActive(show);

        menu.SetActive(!show);
        buttons.SetActive(!show);
    }

    private void HideAll()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    // Alert

    private void OnConfirmButtonClick() => OnAlertButtonClick(true);
    private void OnCancelButtonClick()  => OnAlertButtonClick(false);

    private void OnAlertButtonClick(bool confirm)
    {
        _onResult?.Invoke(confirm);

        HideAlert();
    }
}
