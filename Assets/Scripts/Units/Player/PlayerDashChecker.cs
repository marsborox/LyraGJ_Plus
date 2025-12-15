using UnityEngine;

public class PlayerDashChecker : MonoBehaviour
{
    public bool isPointingAtWall = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        isPointingAtWall=true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
            isPointingAtWall = false;
    }
}
