using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton/*Persistent*/<MySceneManager>
{
    public static new MySceneManager instance => Singleton/*Persistent*/<MySceneManager>.instance;

    public static string PreviousScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }
    public void OpenMainMenu()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        //MySoundManager.instance.PlayMenuMusic();
    }
    public void OpenLobbyScene()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LobbyScene");
        Time.timeScale = 1f;
        MySoundManager.instance.PlayLobbyMusic();
    }
    public void OpenDarkLobbyScene()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("DarkLobbyScene");
        Time.timeScale = 1f;
        MySoundManager.instance.PlayLobbyMusic();
    }
    public void OpenGameScene()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
       //SceneManager.LoadScene("MarosGameScene");
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
        MySoundManager.instance.PlayJazzMusic();
    }
    public void OpenTutorialScene()
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
    public void QuitToWindows()
    {
        Application.Quit();
    }
    public void OpenMarosTestScene()
    {
        // SceneManager.LoadScene("MarosGameScene");
        //SceneManager.LoadScene("MarosGameScene");
        // MySoundManager.instance.PlayJazzMusic();
    }
}
