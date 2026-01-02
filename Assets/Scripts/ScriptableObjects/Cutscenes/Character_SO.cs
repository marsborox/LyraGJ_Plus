using UnityEngine;

[System.Serializable]
public struct EmotionPair
{
    public CharacterEmotion emotion;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "Character_SO", menuName = "Scriptable Objects/Character_SO")]
public class Character_SO : ScriptableObject
{
    public EmotionPair[] portraits;
}
