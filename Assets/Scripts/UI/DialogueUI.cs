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

    private Animator leftAnimator;
    private Animator rightAnimator;

    private void Awake()
    {
        if (leftCharacterImage != null) 
        {
            leftAnimator = leftCharacterImage.GetComponent<Animator>();
        }
        if (rightCharacterImage != null) 
        {
            rightAnimator = rightCharacterImage.GetComponent<Animator>();
        }

        continueButton.onClick.AddListener(ContinueButtonClick);
    }
    public void Show(Sprite image, string text, bool isOnLeftSide = true)
    {
        textOfDialogue.text = text;

        leftCharacterImage.gameObject.SetActive(isOnLeftSide);
        rightCharacterImage.gameObject.SetActive(!isOnLeftSide);

        if (isOnLeftSide)
        {
            if (leftCharacterImage.sprite == image) return;

            leftCharacterImage.sprite = image;
            rightCharacterImage.sprite = null;

            leftAnimator.ResetTrigger("PlayHeadBob");
            leftAnimator.SetTrigger("PlayHeadBob");
        } else
        {
            if (rightCharacterImage.sprite == image) return;

            leftCharacterImage.sprite = null;
            rightCharacterImage.sprite = image;

            rightAnimator.ResetTrigger("PlayHeadBob");
            rightAnimator.SetTrigger("PlayHeadBob");
        }
    }
    private void ContinueButtonClick()
    {
        OnContinueDialogueClicked?.Invoke();
    }
}
