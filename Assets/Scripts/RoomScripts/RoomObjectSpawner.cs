using UnityEngine;

public class RoomObjectSpawner : MonoBehaviour
{
    public ScenePortal portalPrefab;

    public void SpawnPortal(Room room)
    { 
        ScenePortal portal = Instantiate(portalPrefab);
        portal.transform.SetParent(room.transform);
        portal.transform.position = room.transform.position;
        portal.gameScene = GameScene.LOBBY;
    }
}
