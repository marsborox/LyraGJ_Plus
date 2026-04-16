using UnityEngine;
using UnityEngine.UI;

public class ButtonClicking : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        MySoundManager.instance.PlayButtonClickSound();
    }
}
