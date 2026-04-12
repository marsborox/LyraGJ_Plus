using UnityEngine;
using UnityEngine.Tilemaps;
public class CameraFollowing : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform targetToFollow;
    public float zoom = 0.5f;
    public float speed = 5f;
    public Vector2 offset = new Vector2(0f, 0f);

    [Header("Bounds")]
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public RoomManager roomManager; // to automatically calculate camera's bounds

    void Start()
    {
        Camera.main.orthographicSize *= zoom;
        if (roomManager != null)
        {
            roomManager.roomWasSpawned += FindRoomBounds;
            FindRoomBounds(); // it might have subscribed too late
        }

        MoveCamera();
    }
    void OnDestroy()
    {
        if (roomManager != null)
        {
            roomManager.roomWasSpawned -= FindRoomBounds;
        }        
    }
    void LateUpdate()
    {
        MoveCamera();
    }
    void OnDrawGizmos()
    {
        float camHalfHeight = Camera.main != null ? Camera.main.orthographicSize : 0;
        float camHalfWidth  = Camera.main != null ? Camera.main.orthographicSize * Camera.main.aspect : 0;

        // Outer bounds (your scene edge)
        Gizmos.color = Color.red;
        DrawRect(minBounds, maxBounds);

        // Inner bounds (where camera center is actually clamped to)
        Gizmos.color = Color.yellow;
        DrawRect(
            new Vector2(minBounds.x + camHalfWidth,  minBounds.y + camHalfHeight),
            new Vector2(maxBounds.x - camHalfWidth,  maxBounds.y - camHalfHeight)
        );
    }
    void DrawRect(Vector2 min, Vector2 max)
    {
        Vector3 topLeft     = new Vector3(min.x, max.y, 0);
        Vector3 topRight    = new Vector3(max.x, max.y, 0);
        Vector3 bottomLeft  = new Vector3(min.x, min.y, 0);
        Vector3 bottomRight = new Vector3(max.x, min.y, 0);

        Gizmos.DrawLine(topLeft,     topRight);
        Gizmos.DrawLine(topRight,    bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft,  topLeft);
    }    

    private void MoveCamera()
    {
        if (targetToFollow == null) return;

        Vector3 desiredPosition = targetToFollow.position + new Vector3(offset.x, offset.y, transform.position.z);
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, speed * Time.unscaledDeltaTime);

        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth  = Camera.main.orthographicSize * Camera.main.aspect;
        float clampedX = Mathf.Clamp(smoothed.x, minBounds.x + camHalfWidth,  maxBounds.x - camHalfWidth);
        float clampedY = Mathf.Clamp(smoothed.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
    private void FindRoomBounds()
    {
        Tilemap[] tilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
        
        if (tilemaps.Length == 0) return;

        Bounds combined = new Bounds();
        bool initialized = false;

        foreach (Tilemap tm in tilemaps)
        {
            tm.CompressBounds();
            Bounds worldBounds = tm.localBounds;
            worldBounds.center = tm.transform.TransformPoint(worldBounds.center);

            if (!initialized)
            {
                combined = worldBounds;
                initialized = true;
            }
            else
            {
                combined.Encapsulate(worldBounds);
            }
        }

        minBounds = combined.min;
        maxBounds = combined.max;
    }
}