using UnityEngine;
using System.Collections;

public class EnemyDance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private float startAlpha = 0.5f;
    [SerializeField] private float danceDuration = 2.7f;
    public void StartDancing(Type enemyType)
    {
        switch (enemyType)
        {
            case Type.RED: 
                animator.SetBool("Meelee", true);
                break;
            case Type.BLUE: 
                animator.SetBool("Mage", true);
                break;
            case Type.GREEN: 
                animator.SetBool("Ranged", true);
                break;
        }

        StartCoroutine(StopDancing(danceDuration));
    }

    private IEnumerator StopDancing(float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0, elapsed / duration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
