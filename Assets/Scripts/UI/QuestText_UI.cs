using TMPro;
using UnityEngine;

public class QuestText_UI : UI
{
    [SerializeField] private TextMeshProUGUI _questTextText;

    void Update()
    {
        _questTextText.text = GameManager.instance.levelSettings.DisplayQuestText();
    }
}
