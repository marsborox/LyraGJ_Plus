using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonTester : MonoBehaviour
{
    public Button myButton;
    public string sceneToLoad = "YourSceneName";

    void Start()
    {
        if (myButton == null) Debug.LogWarning("myButton not assigned!");
        else myButton.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        Debug.Log("Button clicked (listener). Loading: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}