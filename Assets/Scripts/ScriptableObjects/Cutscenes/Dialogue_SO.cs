using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialoguePart
{
    [TextArea(3, 10)]
    public string dialogueText;  // Supports <b></b>, <i></i>, etc.
    public CharacterID character;
    public CharacterEmotion emotion;
}

[CreateAssetMenu(fileName = "Dialogue_SO", menuName = "Scriptable Objects/Dialogue_SO")]
public class Dialogue_SO : ScriptableObject
{
    public DialoguePart[] parts;
}
