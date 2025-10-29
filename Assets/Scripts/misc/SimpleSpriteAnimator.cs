using UnityEngine;
using System.Collections;

public class SimpleSpriteAnimator : MonoBehaviour
{
    public Sprite[] frames;

    public float frameRate = 0.1f;

    private SpriteRenderer spriteRenderer;
    private bool isAnimating = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Play()
    {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        if (isAnimating) yield break;
        isAnimating = true;

        for (int i = 0; i < frames.Length; i++)
        {
            spriteRenderer.sprite = frames[i];
            yield return new WaitForSeconds(frameRate);
        }
        spriteRenderer.sprite = null;

        isAnimating = false;
    }
}
