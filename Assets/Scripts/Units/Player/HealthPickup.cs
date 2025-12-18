using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            int healAmount = player.unitCombat.healthMax / 5;
            player.GetHeal(healAmount);

            Destroy(gameObject);
        }
    }
}
