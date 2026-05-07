using UnityEngine;

public class RoomObjectSpawner : MonoBehaviour
{
    public Portal portalPrefab;

    public void SpawnPortal(Room room)
    { 
        Portal portal = Instantiate(portalPrefab);
        portal.transform.SetParent(room.transform);
        portal.transform.position = room.transform.position;
        portal.gameScene = GameScene.JAZZ_BOSS;
    }
}
