using UnityEngine;
using UnityEngine.InputSystem;

public class HandleCreditsScene : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            MySceneManager.instance.OpenScene(GameScene.MAIN_MENU);
        }
    }
}
