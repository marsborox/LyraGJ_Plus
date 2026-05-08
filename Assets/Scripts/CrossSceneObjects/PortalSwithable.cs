using UnityEngine;

public class PortalSwithable : Portal
{
    [SerializeField] private Collider2D _myCollider;
    [SerializeField] private SpriteRenderer _mySpriteRenderer;
    [SerializeField] private Sprite _activeSprite;

    void Awake()
    {
        _myCollider = GetComponent<Collider2D>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        GlobalEventManager.OnRoomCleared += MakeActive;
        
    }
    void OnDisable()
    {
        GlobalEventManager.OnRoomCleared -= MakeActive;
    }
    void MakeActive(Room room)
    {
        _myCollider.enabled = true;
        _mySpriteRenderer.sprite = _activeSprite;
    }
}
