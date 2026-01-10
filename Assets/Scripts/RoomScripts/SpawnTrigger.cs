using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Room _roomImIn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //_roomImIn.SpawnEnemies();
            Debug.Log("spawn trigger activated in room: "+_roomImIn.roomID);
            GlobalEventManager.instance.TriggerOnPlayerEnterRoom(_roomImIn);
            //Debug.Log("spawn trigger postActivation in room: " + _roomImIn.roomID);
        }
    }
}
