using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicValue;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI soundsValue;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private Button closeButton;

    void OnEnable()
    {
        if (musicSlider != null) {
            musicSlider.value = MySoundManager.instance.musicVolume;
            musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        }

        if (soundsSlider != null) {
            soundsSlider.value = MySoundManager.instance.soundEffectsVolume;
            soundsSlider.onValueChanged.AddListener(OnSoundsSliderChanged);
        }

        if (closeButton != null) {
            closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        CalculateValues();
    }
    void OnDisable()
    {
        if (musicSlider != null) {
            musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
        }

        if (soundsSlider != null) {
            soundsSlider.onValueChanged.RemoveListener(OnSoundsSliderChanged);
        }

        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        CalculateValues();
    }
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
    private void OnCloseButtonClick()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
    private void CalculateValues()
    {
        if (musicValue != null) {
            float music = Mathf.RoundToInt(MySoundManager.instance.musicVolume * 100f);
            musicValue.text = music + "%";
        }

        if (soundsValue != null) {
            float sounds = Mathf.RoundToInt(MySoundManager.instance.soundEffectsVolume * 100f);
            soundsValue.text = sounds + "%";
        }
    }
}
