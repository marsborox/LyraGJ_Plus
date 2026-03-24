using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public bool IsMenuOpen()
    {
        return gameObject.activeInHierarchy;
    }
    public void OpenMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
