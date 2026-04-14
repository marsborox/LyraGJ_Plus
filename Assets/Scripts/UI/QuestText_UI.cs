using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class QuestText_UI : UI
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI questTextText;
    [SerializeField] private float secondsToBeVisible;

    private string _previousText;

    void Update()
    {
        questTextText.text = GameManager.instance.levelSettings.DisplayQuestText();
        if (questTextText.text != _previousText)
        {
            StartCoroutine(ShowQuest(true, 0));

            _previousText = questTextText.text;
            StartCoroutine(ShowQuest(false, secondsToBeVisible));
        }
    }

    private IEnumerator ShowQuest(bool show, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0;
        float duration = 0.6f;
        questTextText.enabled = false;
        backgroundImage.fillAmount = show ? 0 : 1;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float easedT = t * t;

            backgroundImage.fillAmount = Mathf.Lerp(show ? 0 : 1, show ? 1 : 0, easedT);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        backgroundImage.fillAmount = show ? 1 : 0;
        questTextText.enabled = show;
    }
}
