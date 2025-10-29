using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonPersistent<MySceneManager>
{
    public static new MySceneManager instance => SingletonPersistent<MySceneManager>.instance;
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        MySoundManager.instance.PlayMenuMusic();
    }

    public void OpenGameScene()
    {
        SceneManager.LoadScene("GameScene");
        //MyGameManager.instance.pauseMenu = FindAnyObjectByType<gamePause_UI>();
        MySoundManager.instance.PlayGameMusic();
    }
    public void QuitToWindows()
    {
        Application.Quit();
    }
}
