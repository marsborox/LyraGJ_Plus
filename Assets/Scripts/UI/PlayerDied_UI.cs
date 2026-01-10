using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDied_UI : UI
{
    [SerializeField] private Image _deathSprite;
    [SerializeField] private Button _reloadSceneButton;
    [SerializeField] private bool _isFading = false;
    [SerializeField] private float _fadeTime = 1f;
    [SerializeField] private float alpha;
    private void Start()
    {
        InitiateButton(_reloadSceneButton, ReloadLevel);
    }
    private void Update()
    {
        Fading();
    }
    private void OnEnable()
    {
        StartFading();
    }
    private void StartFading()
    {
        alpha = 0f;
        _deathSprite.color = new Color32(255,255,255,(byte)alpha);
        _isFading=true;
        _reloadSceneButton.gameObject.SetActive(false);
    }
    private void Fading()
    {
        if (_isFading)
        {
            //float alpha = _deathSprite.color.a;
            float alphaIncrement = Time.unscaledDeltaTime * (255 /  _fadeTime);
            alpha = alpha + alphaIncrement;

            if (alpha > 255)
            { 
                alpha = 255;
                _isFading = false;
                _reloadSceneButton.gameObject.SetActive(true);
            }
            _deathSprite.color = new Color32(255,255,255,(byte)alpha);
            //Debug.Log("alpha is: "+alpha);
            //Debug.Log("alphaIncrement is: "+alphaIncrement);
        }
    }
    public void ReloadLevel()
    {
        if (MySceneManager.PreviousScene == "DarkLobbyScene")
        {
            MySceneManager.instance.OpenDarkLobbyScene();
        }
        else
        {
            MySceneManager.instance.OpenLobbyScene();
        }

        // replace with the following code for infinite level restarts
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Time.timeScale = 1f;
    }
}
