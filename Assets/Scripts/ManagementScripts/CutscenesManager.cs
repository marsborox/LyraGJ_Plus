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

public class CutscenesManager : MonoBehaviour
{
    public Level_SO level;
    [SerializeField] private CharacterPortrait_SO[] characterPortraits;
    [SerializeField] private DialogueUI dialogueUI;
    private Dialogue_SO _currentDialogue;
    private int _currentPartIndex = 0;

    public void SpawnDialogue(int indexOfClearedRoom)
    {
        _currentDialogue = null;
        foreach (DialogueToIndex dialogueToIndex in level.dialogueWRoomClearedIndexList)
        {
            if (dialogueToIndex.spawnOnRoomCleared == indexOfClearedRoom)
            {
                _currentDialogue = dialogueToIndex.dialogue;
                _currentPartIndex = 0;
     
                Time.timeScale = 0f; // pause
                dialogueUI.gameObject.SetActive(true);

                SetupDialogue();
                break;
            }
        }
    }
    private void SetupDialogue()
    {
        if (_currentDialogue == null || _currentPartIndex > _currentDialogue.parts.Length - 1)
        {
            dialogueUI.gameObject.SetActive(false);
            Time.timeScale = 1f; // unpause
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
    public void ContinueDialogue()
    {
        if (_currentDialogue == null) return;
        _currentPartIndex++;

        SetupDialogue();
    }
}
