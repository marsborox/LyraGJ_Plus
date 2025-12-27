using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Room _roomImIn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _roomImIn.SpawnEnemies();
            GlobalEventManager.instance.TriggerOnPlayerEnterRoom(_roomImIn);
        }
    }
}
