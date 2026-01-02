using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : UI
{
    public TextMeshProUGUI textOfDialogue;
    public Image leftCharacterImage;
    public Image rightCharacterImage;

    [SerializeField] private Button _closeDialogueButton;


    private void Start()
    {
        InitiateButton(_closeDialogueButton, CloseUI);
    }

    public void CloseUI()
    {
        //temp shutdown for dialogue refactor
        GameManager.instance.ContinueDialogue();
        //GameManager.instance.ContinueDialog();
    }
}
