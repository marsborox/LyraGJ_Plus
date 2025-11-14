using UnityEngine;

public enum GameScene {LOBBY, JAZZ}
public class ScenePortal : MonoBehaviour
{
    public GameScene gameScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        }
    }
}
