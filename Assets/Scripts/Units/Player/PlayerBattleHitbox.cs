using UnityEngine;

public class PlayerBattleHitbox : MonoBehaviour
{
    [SerializeField] private PlayerCombat _playerCombat;
    private void OnTriggerEnter2D(Collider2D other)
    {
        _playerCombat.OnTriggerEnter2DCustom(other);
    }
}
