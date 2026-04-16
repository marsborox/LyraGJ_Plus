using UnityEngine;
using System;
using System.Collections.Generic;
using static Level_SO;

public enum CharacterID
{
    Lyra,
    Muse,
    CharlesRay
}

public enum CharacterEmotion
{
    Neutral,
    Curious,
    Determined,
    Happy,
    Nervous,
    Sad,
    Scared
}

[System.Serializable]
public class DialogueTrigger
{
    public Dialogue_SO dialogue;
    public Collider2D collider;

    [HideInInspector] public bool isInside;
}

public class CutscenesPlayer : MonoBehaviour
{
    private static HashSet<string> _seenDialogueKeys = new HashSet<string>(); // TODO: make empty when new game starts

    public Level_SO level;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private CharacterPortrait_SO[] characterPortraits;
    [SerializeField] private List<DialogueTrigger> triggers = new List<DialogueTrigger>();
    [SerializeField] private GameObject[] hideWhileTalking;
    private Dialogue_SO _currentDialogue;
    private Rigidbody2D _playerRigidbody;
    private int _currentPartIndex = 0;
    private Action _onComplete;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player) _playerRigidbody = player.GetComponent<Rigidbody2D>();

    }
    void OnEnable()
    {
        if (dialogueUI != null)
        {
            dialogueUI.OnContinueDialogueClicked.AddListener(ContinueDialogue);
            dialogueUI.OnSkipDialogueClicked.AddListener(EndDialogue);
        }        
    }
    void OnDisable()
    {
        if (dialogueUI != null)
        {
            dialogueUI.OnContinueDialogueClicked.RemoveListener(ContinueDialogue);
            dialogueUI.OnSkipDialogueClicked.RemoveListener(EndDialogue);
        }        
    }
    public void ContinueDialogue()
    {
        if (_currentDialogue == null) return;
        _currentPartIndex++;

        SetupDialogue();
    }
    public void EndDialogue()
    {
        HideOtherUI(false);
        dialogueUI.gameObject.SetActive(false);
        Time.timeScale = 1f; // unpause

        _onComplete?.Invoke();
        _onComplete = null;
    }
    public void SpawnDialogue(int indexOfClearedRoom)
    {
        _currentDialogue = null;
        foreach (DialogueToIndex dialogueToIndex in level.dialogueWRoomClearedIndexList)
        {
            if (dialogueToIndex.spawnOnRoomCleared == indexOfClearedRoom)
            {
                SpawnDialogue(dialogueToIndex.dialogue, null);
                break;
            }
        }
    }
    public void SpawnDialogue(Dialogue_SO dialogue, Action onComplete)
    {
        _onComplete = onComplete;

        if (!dialogue.showAgain && WasSeenDialogue(dialogue))
        {
            _onComplete?.Invoke();
            _onComplete = null;
            return;
        }

        MarkSeenDialogue(dialogue);

        _currentDialogue = dialogue;
        _currentPartIndex = 0;

        Time.timeScale = 0f; // pause
        HideOtherUI(true);
        dialogueUI.gameObject.SetActive(true);

        SetupDialogue();
    }
    void FixedUpdate()
    {
        CheckTriggers();
    }

    // UI handling

    private void SetupDialogue()
    {
        if (_currentDialogue == null || _currentPartIndex > _currentDialogue.parts.Length - 1)
        {
            EndDialogue();
            return;
        }

        DialoguePart part = _currentDialogue.parts[_currentPartIndex];
        bool stop = false;
        foreach (CharacterPortrait_SO characterPortrait in characterPortraits)
        {
            if (characterPortrait.character == part.character)
            {
                foreach (EmotionPair emotionPair in characterPortrait.portraits)
                {
                    if (emotionPair.emotion == part.emotion)
                    {
                        dialogueUI.Show(emotionPair.sprite, part.dialogueText, part.character == CharacterID.Lyra, _currentPartIndex < _currentDialogue.parts.Length - 1);              
                        stop = true;
                        break;
                    }
                }

                if (stop) break;
            }
        }
    }
    private void HideOtherUI(bool hide)
    {
        foreach (GameObject gameObject in hideWhileTalking)
        {
            if (gameObject != null) gameObject.SetActive(!hide);
        }
    }

    // triggers handling

    private void CheckTriggers()
    {
        if (triggers.Count == 0 || _playerRigidbody == null) return; // nothing to check

        Vector3 playerPosition = _playerRigidbody.position;

        foreach (var trigger in triggers)
        {
            bool inside = trigger.collider.bounds.Contains(playerPosition);

            if (inside && !trigger.isInside)
            {
                trigger.isInside = true;
                OnTriggerEntered(trigger);
                break;
            }
            else if (!inside && trigger.isInside)
            {
                trigger.isInside = false;
                OnTriggerExited(trigger);
                break;
            }
        }
    }
    private void OnTriggerEntered(DialogueTrigger trigger)
    {
        // triggers are to be run just once
        SpawnDialogue(trigger.dialogue, () => triggers.Remove(trigger));
    }
    private void OnTriggerExited(DialogueTrigger trigger)
    {
        // do nothing
    }

    // dialogues persistence

    private bool WasSeenDialogue(Dialogue_SO dialogue)
    {
        string dialogueKey = DialogueKey(dialogue);
        return _seenDialogueKeys.Contains(dialogueKey);
    }
    private void MarkSeenDialogue(Dialogue_SO dialogue)
    {
        string dialogueKey = DialogueKey(dialogue);
        _seenDialogueKeys.Add(dialogueKey);
    }
    private string DialogueKey(Dialogue_SO dialogue)
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        return $"{sceneName}_{dialogue.fileName}";
    }
}
