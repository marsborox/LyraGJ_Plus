using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _maxHealthRegenPercentage = 20;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            int healAmount = player.unitCombat.healthMax / (100/_maxHealthRegenPercentage);
            player.GetHeal(healAmount);

            Destroy(gameObject);
        }
    }
}
