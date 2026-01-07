using UnityEngine;
using static Level_SO;

public enum CharacterID
{
    Lyra,
    Muse
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
    public int dialogueID;
    public Collider2D collider;

    [HideInInspector] public bool isInside;
}

public class CutscenesManager : MonoBehaviour
{
    public Level_SO level;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private CharacterPortrait_SO[] characterPortraits;
    [SerializeField] private DialogueTrigger[] triggers;
    [SerializeField] private GameObject[] hideWhileTalking;
    private Dialogue_SO _currentDialogue;
    private int _currentPartIndex = 0;
    private Rigidbody2D _playerRigidbody;

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            _playerRigidbody = player.GetComponent<Rigidbody2D>();
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
    }
    public void SpawnDialogue(int indexOfClearedRoom)
    {
        Debug.Log("Spawning!!!");
        _currentDialogue = null;
        foreach (DialogueToIndex dialogueToIndex in level.dialogueWRoomClearedIndexList)
        {
            if (dialogueToIndex.spawnOnRoomCleared == indexOfClearedRoom)
            {
                _currentDialogue = dialogueToIndex.dialogue;
                _currentPartIndex = 0;
     
                Time.timeScale = 0f; // pause
                HideOtherUI(true);
                dialogueUI.gameObject.SetActive(true);

                SetupDialogue();
                break;
            }
        }
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
                        dialogueUI.Show(emotionPair.sprite, part.dialogueText, part.character == CharacterID.Lyra);              
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
            if (gameObject != null)
            {
                gameObject.SetActive(!hide);
            }
        }
    }

    // triggers handling

    private void CheckTriggers()
    {
        if (triggers.Length == 0 || _playerRigidbody == null) return; // nothing to check

        Vector3 playerPosition = _playerRigidbody.position;

        foreach (var trigger in triggers)
        {
            bool inside = trigger.collider.bounds.Contains(playerPosition);

            if (inside && !trigger.isInside)
            {
                trigger.isInside = true;
                OnTriggerEntered(trigger);
            }
            else if (!inside && trigger.isInside)
            {
                trigger.isInside = false;
                OnTriggerExited(trigger);
            }
        }
    }

    private void OnTriggerEntered(DialogueTrigger trigger)
    {
        SpawnDialogue(trigger.dialogueID);
    }

    private void OnTriggerExited(DialogueTrigger trigger)
    {
        // do nothing
    }
}
