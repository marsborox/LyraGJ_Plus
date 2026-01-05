using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class DialogueUI : UI
{
    public UnityEvent OnContinueDialogueClicked;

    [SerializeField] private TextMeshProUGUI textOfDialogue;
    [SerializeField] private Image leftCharacterImage;
    [SerializeField] private Image rightCharacterImage;
    [SerializeField] private Button continueButton;

    private Animator _leftAnimator;
    private Animator _rightAnimator;

    // typing properties
    private float _delayTyping = 0.02f;
    private string _messageToType = "";
    private void Awake()
    {
        if (leftCharacterImage != null) 
        {
            _leftAnimator = leftCharacterImage.GetComponent<Animator>();
        }
        if (rightCharacterImage != null) 
        {
            _rightAnimator = rightCharacterImage.GetComponent<Animator>();
        }

        continueButton.onClick.AddListener(ContinueButtonClick);
    }
    public void Show(Sprite image, string text, bool isOnLeftSide = true)
    {
        _messageToType = text;
        textOfDialogue.text = "";

        leftCharacterImage.gameObject.SetActive(isOnLeftSide);
        rightCharacterImage.gameObject.SetActive(!isOnLeftSide);

        if (isOnLeftSide)
        {
            rightCharacterImage.sprite = null;

            if (leftCharacterImage.sprite != image)
            {
                leftCharacterImage.sprite = image;
                
                if (_leftAnimator != null)
                {
                    _leftAnimator.ResetTrigger("PlayHeadBob");
                    _leftAnimator.SetTrigger("PlayHeadBob");
                }
            }
        } else
        {
            leftCharacterImage.sprite = null;

            if (rightCharacterImage.sprite != image)
            {
                rightCharacterImage.sprite = image;

                if (_rightAnimator != null)
                {
                    _rightAnimator.ResetTrigger("PlayHeadBob");
                    _rightAnimator.SetTrigger("PlayHeadBob");
                }
            }
        }

        StopAllCoroutines();
        StartCoroutine(TypeMessage());
    }
    private IEnumerator TypeMessage()
    {
        for (int i = 0; i < _messageToType.Length; i++)
        {
            textOfDialogue.text += _messageToType[i];
            yield return new WaitForSecondsRealtime(_delayTyping);
        }
    }
    private void ContinueButtonClick()
    {
        if (textOfDialogue.text.Length < _messageToType.Length)
        {
            StopAllCoroutines();
            textOfDialogue.text = _messageToType;
        } else 
        {
            OnContinueDialogueClicked?.Invoke();
        }
    }
}
