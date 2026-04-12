using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class DialogueUI : UI
{
    public UnityEvent OnContinueDialogueClicked;
    public UnityEvent OnSkipDialogueClicked;

    [SerializeField] private TextMeshProUGUI textOfDialogue;
    [SerializeField] private Image leftCharacterImage;
    [SerializeField] private Image rightCharacterImage;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Image continueImage;

    private Animator _leftAnimator;
    private Animator _rightAnimator;
    private Coroutine _pressContinueRoutine;
    
    // typing properties
    private bool _isTyping = false;
    private float _delayTyping = 0.02f;
    private string _messageToType = "";
    private Coroutine _typingRoutine;
    private Sprite _previousSprite;

    void Awake()
    {
        if (leftCharacterImage != null) _leftAnimator = leftCharacterImage.GetComponent<Animator>();
        if (rightCharacterImage != null) _rightAnimator = rightCharacterImage.GetComponent<Animator>();
    }
    void OnEnable()
    {
        if (continueButton != null) continueButton.onClick.AddListener(ContinueButtonClick);
        if (skipButton != null) skipButton.onClick.AddListener(SkipButtonClick);
    }
    void OnDisable()
    {
        if (continueButton != null) continueButton.onClick.RemoveListener(ContinueButtonClick);
        if (skipButton != null) skipButton.onClick.RemoveListener(SkipButtonClick);
    }
    public void Show(Sprite image, string text, bool isOnLeftSide = true, bool hasMoreDialogues = true)
    {
        _messageToType = text;
        textOfDialogue.text = "";

        leftCharacterImage.gameObject.SetActive(isOnLeftSide);
        rightCharacterImage.gameObject.SetActive(!isOnLeftSide);

        bool isChangedSpeaker = image != _previousSprite;

        if (isOnLeftSide)
        {
            rightCharacterImage.sprite = null;
            leftCharacterImage.sprite = image;
            
            if (isChangedSpeaker && _leftAnimator != null)
            {
                _leftAnimator.ResetTrigger("PlayHeadBob");
                _leftAnimator.Play("Idle", 0, 0f);
                _leftAnimator.SetTrigger("PlayHeadBob");
            }
        } 
        else
        {
            leftCharacterImage.sprite = null;
            rightCharacterImage.sprite = image;

            if (isChangedSpeaker && _rightAnimator != null)
            {
                _rightAnimator.ResetTrigger("PlayHeadBob");
                _rightAnimator.Play("Idle", 0, 0f);
                _rightAnimator.SetTrigger("PlayHeadBob");
            }
        }

        if (continueImage != null) continueImage.gameObject.SetActive(hasMoreDialogues);

        textOfDialogue.text = text; // comment out to enable typing
        _previousSprite = image;

        // if (_typingRoutine != null) StopCoroutine(_typingRoutine);
        // _typingRoutine = StartCoroutine(TypeMessage());
    }
    private IEnumerator TypeMessage()
    {
        _isTyping = true;
        for (int i = 0; i < _messageToType.Length; i++)
        {
            textOfDialogue.text += _messageToType[i];
            yield return new WaitForSecondsRealtime(_delayTyping);
        }
        _isTyping = false;
    }
    private void ContinueButtonClick()
    {
        ToggleContinueImage();

        if (_isTyping)
        {
            _isTyping = false;

            if (_typingRoutine != null) StopCoroutine(_typingRoutine);
            textOfDialogue.text = _messageToType;
        } else 
        {
            OnContinueDialogueClicked?.Invoke();
        }
    }
    private void SkipButtonClick()
    {
        OnSkipDialogueClicked?.Invoke();
    }

    // handle continue image coloring

    private void ToggleContinueImage()
    {
        if (_pressContinueRoutine != null) StopCoroutine(_pressContinueRoutine);
        _pressContinueRoutine = StartCoroutine(PressContinueSequence());
    }
    private IEnumerator PressContinueSequence()
    {
        PressContinueImage(true);
        yield return new WaitForSecondsRealtime(0.12f);
        PressContinueImage(false);
    }
    private void PressContinueImage(bool press)
    {
        Color c = continueImage.color;
        c.a = press ? 0.9f : 1f;
        continueImage.color = c;
    }
}
