using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameScene gameScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.instance != null) GameManager.instance.PostLevelClear();
            MySceneManager.instance.OpenScene(gameScene);
        }
    }
}
