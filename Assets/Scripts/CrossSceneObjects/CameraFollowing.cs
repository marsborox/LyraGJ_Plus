using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Transform targetToFollow;
    public float zoom = 0.5f;
    public float speed = 5f;
    public Vector2 offset = new Vector2(0f, 0f);

    private Camera camera;

    void Start()
    {
        SetFollowPlayer();
        camera = GetComponent<Camera>();
        if (camera != null && camera.orthographic) {
            camera.orthographicSize *= zoom;
        }

        MoveCamera();
        
    }


    void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (targetToFollow == null) return;

        Vector3 desiredPosition = targetToFollow.position + new Vector3(offset.x, offset.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }
    private void SetFollowPlayer()
    { 
        targetToFollow = PlayerSingleton.instance.transform;
    }
}