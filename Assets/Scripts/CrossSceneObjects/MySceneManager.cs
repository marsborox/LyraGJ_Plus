using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameScene {LOBBY, DARKLOBBY, JAZZ, JAZZ_BOSS, TUTORIAL, TESTCOMBAT, DEV, MAIN_MENU, DRESSING_ROOM}

public class MySceneManager : Singleton/*Persistent*/<MySceneManager>
{
    public static new MySceneManager instance => Singleton/*Persistent*/<MySceneManager>.instance;
    public static string PreviousScene { get; private set; }

    private static bool didPlayerSurviveTutorial = false;

    [Header("Dialog References")]
    [SerializeField] private PauseUI pauseUI;

    [Header("Fade Scene Settings")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Color fadeColor = Color.black;

    public bool isFadingInProgress { get { return _fadeCanvasGroup.alpha > 0f; } }

    private CanvasGroup _fadeCanvasGroup;
    private static bool _isFirstLoad = true;
    private static bool _pendingFadeIn = false;
    
    // frames skipping - when loading a new scene, sometimes it takes a few hundred ms
    private const int FADE_IN_SKIP_FRAMES = 5;
    private static int _skipNextFrames = 0;

    protected override void Awake()
    {
        base.Awake();

        CreateOrFindFadeCanvas();
    }
    void Start()
    {
        if (instance != this) return;

        StartFadingIn();
    }
    void Update()
    {
        FinishFadingIn();
    }
    public void OpenScene(GameScene scene)
    {
        StartCoroutine(FadeAndLoad(scene));
    }

    public void OpenPauseMenu()
    {
        if (isFadingInProgress) return;

        pauseUI.ToggleMenu();
    }

    public void OpenOptions()
    {
        if (isFadingInProgress) return;

        pauseUI.OpenMenu();
        pauseUI.ShowOptions();
    }

    // Scenes code

    public void StartGame()
    {
        OpenScene(GameScene.DARKLOBBY);
    }
    public void QuitToWindows()
    {
        Application.Quit();
    }
    public void OpenLobby()
    {
        if (PreviousScene == "DarkLobbyScene")
        {
            OpenScene(GameScene.DARKLOBBY);
        } else
        {
            OpenScene(GameScene.LOBBY);
        }
    }

    public void OpenElevator()
    {
        if (didPlayerSurviveTutorial)
        {
            OpenScene(GameScene.JAZZ);
        } else
        {
            OpenScene(GameScene.TUTORIAL);
        }
    }

    private void OpenMainMenu()
    {
        PreviousScene = null; // nothing to go back to
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        MySoundManager.instance.StopMusic();
    }
    private void OpenLobbyScene()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LobbyScene");
        Time.timeScale = 1f;
        MySoundManager.instance.PlayLobbyMusic();
    }
    private void OpenDarkLobbyScene()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("DarkLobbyScene");
        Time.timeScale = 1f;
        MySoundManager.instance.PlayLobbyMusic();
    }
    private void OpenGameScene()
    {
        didPlayerSurviveTutorial = true; // must have, right?

        PreviousScene = SceneManager.GetActiveScene().name;
       //SceneManager.LoadScene("MarosGameScene");
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
        MySoundManager.instance.PlayJazzMusic();
    }
    private void OpenJazzBossScene()
    {
        PreviousScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene("BossScene");
        Time.timeScale = 1f;
        MySoundManager.instance.PlayJazzMusic();
    }
    private void OpenTutorialScene()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("TutorialScene");
        Time.timeScale = 1f;
        MySoundManager.instance.PlayJazzMusic();
    }
    public void OpenDevScene()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("MarosGameScene");
        Time.timeScale = 1f;
        MySoundManager.instance.PlayJazzMusic();
    }
    private void OpenMarosTestScene()
    {
        // SceneManager.LoadScene("MarosGameScene");
        //SceneManager.LoadScene("MarosGameScene");
        // MySoundManager.instance.PlayJazzMusic();
    }
    private void OpenDressingRoomScene()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("DressingRoomScene");
        Time.timeScale = 1f;
    }

    // Fading

    private IEnumerator FadeAndLoad(GameScene scene)
    {
        yield return StartCoroutine(FadeOut(fadeDuration));

        _pendingFadeIn = true;
        _skipNextFrames = FADE_IN_SKIP_FRAMES;

        switch (scene)
        {
            case GameScene.LOBBY:
                OpenLobbyScene();
                break;
            case GameScene.DARKLOBBY:
                OpenDarkLobbyScene();
                break;
            case GameScene.JAZZ:
                OpenGameScene();
                break;
            case GameScene.JAZZ_BOSS:
                OpenJazzBossScene();
                break;
            case GameScene.TUTORIAL:
                OpenTutorialScene();
                break;
            case GameScene.TESTCOMBAT:
                OpenMarosTestScene();
                break;
            case GameScene.DEV:
                OpenDevScene();
                break;
            case GameScene.MAIN_MENU:
                OpenMainMenu();
                break;
            case GameScene.DRESSING_ROOM:
                OpenDressingRoomScene();
                break;
        }
    }

    private IEnumerator FadeOut(float duration)
    {
        yield return Fade(0f, 1f, duration);

        _fadeCanvasGroup.blocksRaycasts = true;
    }
    private void StartFadingIn()
    {
        if (_isFirstLoad)
        {
            _isFirstLoad = false;
            StartCoroutine(FadeIn(0));
        }
    }
    private void FinishFadingIn()
    {
        if (_pendingFadeIn)
        {
            if (_skipNextFrames > 0)
            {
                _skipNextFrames--;
                
            } 
            else
            {
                _pendingFadeIn = false;
                _skipNextFrames = 0;
                StartCoroutine(FadeIn(fadeDuration));
            }
        }
    }
    private IEnumerator FadeIn(float duration)
    {
        yield return Fade(1f, 0f, duration);

        _fadeCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        if (_fadeCanvasGroup == null) yield break;

        if (duration <= 0f)
        {
            _fadeCanvasGroup.alpha = to;
            yield break;
        }

        float elapsed = 0f;
        _fadeCanvasGroup.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            _fadeCanvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        _fadeCanvasGroup.alpha = to;
    }
    private void CreateOrFindFadeCanvas()
    {
        GameObject existing = GameObject.Find("FadeCanvas");
        if (existing != null)
        {
            _fadeCanvasGroup = existing.GetComponentInChildren<CanvasGroup>();
            if (_fadeCanvasGroup != null)
            {
                return;
            }
        }

        CreateFadeCanvas();
    }
    private void CreateFadeCanvas()
    {
        GameObject canvasGO = new GameObject("FadeCanvas");
        canvasGO.transform.SetParent(transform);

        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999; // always on top

        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        GameObject panelGO = new GameObject("FadePanel");
        panelGO.transform.SetParent(canvasGO.transform, false);

        Image image = panelGO.AddComponent<Image>();
        image.color = fadeColor;

        RectTransform rect = panelGO.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        _fadeCanvasGroup = canvasGO.AddComponent<CanvasGroup>();
        _fadeCanvasGroup.alpha = 1f;
        _fadeCanvasGroup.blocksRaycasts = true;
        _fadeCanvasGroup.interactable = false;
    }
}
