using System.Collections;

using TMPro;

using UnityEngine;

public class EnemyConversation : MonoBehaviour
{
    float closingTime = 1f;
    public TextMeshPro conversationTextField;
    public EnemyConversation_SO enemyConversations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnEnable()
    {
        DisplayRandomText();
        StartCoroutine(CloseConversationRoutine());
    }
    private void OnDisable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void DisplayRandomText()
    {
        int randomIndex = Random.Range(0,enemyConversations.conversationList.Count-1);
        conversationTextField.text = enemyConversations.conversationList[randomIndex].dialogueText;
        
    }
    IEnumerator CloseConversationRoutine()
    {
        yield return new WaitForSeconds(closingTime);
        this.gameObject.SetActive(false);
    }
}
