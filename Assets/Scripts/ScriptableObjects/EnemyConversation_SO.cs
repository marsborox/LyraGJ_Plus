using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConversation_SO", menuName = "Scriptable Objects/EnemyConversation_SO")]
public class EnemyConversation_SO : ScriptableObject
{
    [System.Serializable]
    public class EnemyConversation
    {
        [TextArea(3, 10)]
        public string dialogueText;  // Supports <b></b>, <i></i>, etc.
    }

    public List<EnemyConversation> conversationList = new List<EnemyConversation>();

}
