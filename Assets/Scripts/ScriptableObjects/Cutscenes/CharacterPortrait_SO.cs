using UnityEngine;

[System.Serializable]
public struct EmotionPair
{
    public CharacterEmotion emotion;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "Character_SO", menuName = "Scriptable Objects/Character_SO")]
public class CharacterPortrait_SO : ScriptableObject
{
    public CharacterID character;
    public EmotionPair[] portraits;
}
