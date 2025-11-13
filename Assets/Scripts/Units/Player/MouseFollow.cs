using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        FaceMouse();
    }
    private void FaceMouse()
    {
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.up = -direction;//was right //we had it up and -direction
    }
    public Quaternion ReturnMouseDirection()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z - 90);
        return rotation;
    }
}
