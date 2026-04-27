using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("collision");
        if (other.gameObject.tag == "Wall") Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("trigger myName: " + gameObject.name+" other: "+other.gameObject.name);
        if (other.gameObject.tag == "Wall") Destroy(gameObject);
    }
}
