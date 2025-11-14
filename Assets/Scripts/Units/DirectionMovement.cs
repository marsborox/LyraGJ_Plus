using UnityEngine;

public class DirectionMovement : MonoBehaviour
{
    public Sprite[] sprites;
    [SerializeField] private float _frameDuration = 0.2f;

    private float _time = 0f;
    private int _currentSprite = 0;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Animate(float deltaTime)
    {
        _time += deltaTime;
        if (_time >= _frameDuration)
        {
            _time = 0;
            _currentSprite++;
            if (_currentSprite == sprites.Length)
            {
                _currentSprite = 0;
            }

            if (_currentSprite < sprites.Length && sprites[_currentSprite] != null)
            {
                _spriteRenderer.sprite = sprites[_currentSprite];
            }
        }
    }
}
