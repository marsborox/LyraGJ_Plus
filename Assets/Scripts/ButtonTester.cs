using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonTester : MonoBehaviour
{
    [SerializeField] private Button _myButton;
    public string sceneToLoad = "YourSceneName";

    void Start()
    {
        if (_myButton == null) Debug.LogWarning("_myButton not assigned!");
        else _myButton.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        Debug.Log("Button clicked (listener). Loading: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}