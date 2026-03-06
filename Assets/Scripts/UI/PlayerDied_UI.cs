using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDied_UI : UI
{
    [SerializeField] private Image backgroundOverlay;
    [SerializeField] private Image playerImage;
    [SerializeField] private Animator deathAnimation;
    [SerializeField] private Sprite playerCharacter;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private Button reloadSceneButton;
    [SerializeField] private float fadeTime = 1f;
    
    private float _alpha;
    private bool _isFading = false;

    private void Start()
    {
        InitiateButton(reloadSceneButton, ReloadLevel);
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
        MySoundManager.instance.StopMusic();

        _alpha = 0f;
        UpdateAlpha();

        _isFading = true;
        reloadSceneButton.gameObject.SetActive(false);
        deathAnimation.gameObject.SetActive(false);
    }
    private void Fading()
    {
        if (!_isFading) return;

        //float alpha = _deathSprite.color.a;
        float alphaIncrement = Time.unscaledDeltaTime * (255 /  fadeTime);
        _alpha += alphaIncrement;

        if (_alpha >= 255)
        { 
            _alpha = 255;
            _isFading = false;
            playerImage.gameObject.SetActive(false);
            reloadSceneButton.gameObject.SetActive(true);
            deathAnimation.gameObject.SetActive(true);
            deathAnimation.Play("Death");

            dialogueUI.Show(playerCharacter, "Oh... Let's try again?", true, false);
            dialogueUI.gameObject.SetActive(true);
        }

        UpdateAlpha();
        //Debug.Log("alpha is: "+alpha);
        //Debug.Log("alphaIncrement is: "+alphaIncrement);
    }
    public void ReloadLevel()
    {
        // TODO: rework in Level_SO, e.g. "Death Scene Name"
        if (MySceneManager.PreviousScene == "DarkLobbyScene" || MySceneManager.PreviousScene == "TutorialScene")
        {
            MySceneManager.instance.OpenScene(GameScene.DARKLOBBY);
        }
        else
        {
            MySceneManager.instance.OpenScene(GameScene.LOBBY);
        }

        // replace with the following code for infinite level restarts
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Time.timeScale = 1f;
    }

    private void UpdateAlpha()
    {
        Color c = backgroundOverlay.color;
        c.a = _alpha;
        backgroundOverlay.color = c;
    }
}
