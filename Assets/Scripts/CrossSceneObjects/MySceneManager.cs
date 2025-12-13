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
        Debug.Log("scene changed");
        PlayerSingleton.instance.transform.position = PlayerSpawnPoint.instance.transform.position;//move player to centre of map
        MySoundManager.instance.PlayLobbyMusic();
    }
    public void OpenGameScene()
    {
        SceneManager.LoadScene("MarosGameScene");
        // SceneManager.LoadScene("GameScene");
        Debug.Log("scene changed");
        PlayerSingleton.instance.transform.position = PlayerSpawnPoint.instance.transform.position;//move player to centre of map
        MySoundManager.instance.PlayGameMusic();
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
