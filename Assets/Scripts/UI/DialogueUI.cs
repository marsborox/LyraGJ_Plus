using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : UI
{
    public TextMeshProUGUI textOfDialogue;
    public Image characterImage;

    public Button closeDialogueButton;

    private void Start()
    {
        InitiateButton(closeDialogueButton,CloseUI);
    }

    public void CloseUI()
    {
        GameManager.instance.ContinueDialogue();
    }
}
