using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton/*Persistent*/<MySceneManager>
{
    public static new MySceneManager instance => Singleton/*Persistent*/<MySceneManager>.instance;

    protected override void Awake()
    {
        base.Awake();
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //MySoundManager.instance.PlayMenuMusic();
    }
    public void OpenLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
        //MySoundManager.instance.PlayLobbyMusic();
    }
    public void OpenGameScene()
    {
        //SceneManager.LoadScene("MarosGameScene");
        SceneManager.LoadScene("GameScene");
        //MySoundManager.instance.PlayGameMusic();
    }
    public void OpenTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
        //MySoundManager.instance.PlayGameMusic();//mabye change music
    }
    public void OpenDevScene()
    {
        SceneManager.LoadScene("MarosGameScene");
    }
    public void QuitToWindows()
    {
        Application.Quit();
    }
    public void OpenMarosTestScene()
    {
        // SceneManager.LoadScene("MarosGameScene");
        //SceneManager.LoadScene("MarosGameScene");
    }
}
