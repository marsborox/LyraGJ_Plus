using UnityEngine;
using System.Collections;

public class EnemyDance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
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

    private IEnumerator StopDancing(float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0;
        float duration = 1f;

        while (elapsed < duration)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a * ((duration - elapsed)/duration));
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
