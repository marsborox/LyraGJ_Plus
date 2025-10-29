using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // You can set the scene name in the Inspector
    public string sceneToLoad;

    public void LoadScene()
    {
        Debug.Log("Trying!");
        SceneManager.LoadScene(sceneToLoad);
    }
}