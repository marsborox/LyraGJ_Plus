using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene {LOBBY, DARKLOBBY, JAZZ, TUTORIAL, TESTCOMBAT, DEV}
public class ScenePortal : MonoBehaviour
{
    private static bool didPlayerSurviveTutorial = false;
    public GameScene gameScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Entering portalPrefab");
            switch (gameScene)
            {
                case GameScene.LOBBY:
                    {
                        // TODO: rework this conditions by moving it to Level_SO, e.g. "Portal Scene Name"
                        if (SceneManager.GetActiveScene().name == "TutorialScene" && MySceneManager.PreviousScene == "DarkLobbyScene")
                        {
                            didPlayerSurviveTutorial = true;
                            MySceneManager.instance.OpenGameScene();
                        }
                        else
                        {
                            MySceneManager.instance.OpenLobbyScene();
                        }
                        break;
                    }
                case GameScene.JAZZ:
                    {
                        MySceneManager.instance.OpenGameScene();
                        break;
                    }
                case GameScene.TESTCOMBAT:
                    {
                        MySceneManager.instance.OpenMarosTestScene();
                        break;
                    }
                case GameScene.TUTORIAL:
                    {
                        if (MySceneManager.PreviousScene == "LobbyScene" || !didPlayerSurviveTutorial)
                        {
                            MySceneManager.instance.OpenTutorialScene();                       
                        }
                        else
                        {
                            MySceneManager.instance.OpenGameScene();
                        }
                        break;
                    }
                case GameScene.DEV:
                    {
                        MySceneManager.instance.OpenDevScene();
                        break;
                    }
            }
        }
    }
}
