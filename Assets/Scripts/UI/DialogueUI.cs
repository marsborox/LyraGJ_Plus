using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueUI : UI
{
    public UnityEvent OnContinueDialogueClicked;

    [SerializeField] private TextMeshProUGUI textOfDialogue;
    [SerializeField] private Image leftCharacterImage;
    [SerializeField] private Image rightCharacterImage;

    [SerializeField] private Button continueButton;

    public void Show(Sprite image, string text, bool isOnLeftSide = true)
    {
        textOfDialogue.text = text;

        leftCharacterImage.gameObject.SetActive(isOnLeftSide);
        rightCharacterImage.gameObject.SetActive(!isOnLeftSide);

        if (isOnLeftSide)
        {
            leftCharacterImage.sprite = image;
            rightCharacterImage.sprite = null;
        } else
        {
            leftCharacterImage.sprite = null;
            rightCharacterImage.sprite = image;
        }
    }
    private void Awake()
    {
        continueButton.onClick.AddListener(ContinueButtonClick);
    }
    private void ContinueButtonClick()
    {
        OnContinueDialogueClicked?.Invoke();
    }
}
