using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class HandleCreditsScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            MySceneManager.instance.OpenScene(GameScene.MAIN_MENU);
        }
    }
    void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        
        videoPlayer.Stop();
        videoPlayer.time = 0;
        videoPlayer.Play();
    }

    void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
        videoPlayer.Stop();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        MySceneManager.instance.OpenScene(GameScene.MAIN_MENU);
    }
}
