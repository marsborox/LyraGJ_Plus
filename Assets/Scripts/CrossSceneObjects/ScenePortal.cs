using UnityEngine;
using UnityEngine.SceneManagement;

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
                            MySceneManager.instance.OpenScene(GameScene.JAZZ);
                        }
                        else
                        {
                            MySceneManager.instance.OpenScene(GameScene.LOBBY);
                        }
                        break;
                    }
                case GameScene.JAZZ:
                    {
                        MySceneManager.instance.OpenScene(GameScene.JAZZ);
                        break;
                    }
                case GameScene.TESTCOMBAT:
                    {
                        MySceneManager.instance.OpenScene(GameScene.TESTCOMBAT);
                        break;
                    }
                case GameScene.TUTORIAL:
                    {
                        if (MySceneManager.PreviousScene == "LobbyScene" || !didPlayerSurviveTutorial)
                        {
                            MySceneManager.instance.OpenScene(GameScene.TUTORIAL);
                        }
                        else
                        {
                            MySceneManager.instance.OpenScene(GameScene.JAZZ);
                        }
                        break;
                    }
                case GameScene.DEV:
                    {
                        MySceneManager.instance.OpenScene(GameScene.DEV);
                        break;
                    }
            }
        }
    }
}
