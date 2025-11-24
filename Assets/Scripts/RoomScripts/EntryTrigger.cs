using UnityEngine;

public class EntryTrigger : MonoBehaviour
{
    [SerializeField] private Room _roomImIn;
    public Direction direction;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _roomImIn.TriggerActivated(this);
        }
    }
}
