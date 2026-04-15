using UnityEngine;
using System.Collections;

public class DisappearWhenHit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string tagToDisappear;
    [SerializeField] private float disappearingDuration;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagToDisappear)
        {
            StartCoroutine(Disappear(0.5f));
        }
    }

    private IEnumerator Disappear(float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0;
        float duration = disappearingDuration;

        // duration - elapsed/duration

        while (elapsed < duration)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a * ((duration - elapsed)/duration));
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
