using UnityEngine;
using System.IO;

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
    public bool showAgain = false;
    public DialoguePart[] parts;

    [HideInInspector] public string fileName;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(fileName))
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(this);
            if (!string.IsNullOrEmpty(path))
            {
                fileName = Path.GetFileNameWithoutExtension(path);
            }
        }
    }
}
