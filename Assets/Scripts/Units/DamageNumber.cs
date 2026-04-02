using System.Collections;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [Header("Animation Settings")]
    public float floatSpeed = 1.5f;
    public float fadeDuration = 0.8f;
    public float floatHeight = 1.2f;

    private TextMeshProUGUI _text;
    private Color _startColor;
    

    void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _startColor = _text.color;
    }

    public void Show(float damage, Color color = default)
    {
        if (color != default) _text.color = color;

        _startColor = _text.color;
        _text.text = Mathf.RoundToInt(damage).ToString();
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(Random.Range(-0.3f, 0.3f), floatHeight, 0);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            // Float upward with easing
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));

            // Fade out in the second half
            float alpha = t < 0.5f ? 1f : 1f - ((t - 0.5f) / 0.5f);
            _text.color = new Color(_startColor.r, _startColor.g, _startColor.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }
}