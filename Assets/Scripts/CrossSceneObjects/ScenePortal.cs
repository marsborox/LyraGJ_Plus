using UnityEngine;

public enum GameScene {LOBBY, JAZZ,TUTORIAL,TESTCOMBAT,DEV}
public class ScenePortal : MonoBehaviour
{
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
                        MySceneManager.instance.OpenLobbyScene();
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
                        MySceneManager.instance.OpenTutorialScene();
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
