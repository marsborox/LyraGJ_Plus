using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public GameScene gameScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.PostLevelClear();
            MySceneManager.instance.OpenScene(gameScene);
        }
    }
}
