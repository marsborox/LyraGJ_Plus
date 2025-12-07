using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonPersistent<MySceneManager>
{
    public static new MySceneManager instance => SingletonPersistent<MySceneManager>.instance;

    protected override void Awake()
    {
        base.Awake();
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        MySoundManager.instance.PlayMenuMusic();
    }
    public void OpenLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
        MySoundManager.instance.PlayLobbyMusic();
    }
    public void OpenGameScene()
    {
        SceneManager.LoadScene("GameScene");
        MySoundManager.instance.PlayGameMusic();
    }
    public void QuitToWindows()
    {
        Application.Quit();
    }
    public void OpenMarosTestScene()
    {
        SceneManager.LoadScene("GameScene2");
        //SceneManager.LoadScene("MarosGameScene");
    }
}
