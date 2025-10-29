using UnityEngine;

public class DirectionMovement : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameDuration = 0.2f;

    private float time = 0f;
    private int currentSprite = 0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Animate(float deltaTime)
    {
        time += deltaTime;
        if (time >= frameDuration)
        {
            time = 0;
            currentSprite++;
            if (currentSprite == sprites.Length)
            {
                currentSprite = 0;
            }

            if (currentSprite < sprites.Length && sprites[currentSprite] != null)
            {
                spriteRenderer.sprite = sprites[currentSprite];
            }
        }
    }
}
