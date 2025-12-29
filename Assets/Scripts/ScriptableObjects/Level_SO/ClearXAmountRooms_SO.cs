using UnityEngine;

[CreateAssetMenu(fileName = "ClearXAmountRooms", menuName = "Scriptable Objects/Level_SOs/ClearXAmountRooms")]
public class ClearXAmountRooms_SO : Level_SO
{
    public override void SubscribeToEvents(Room room)
    {
        GlobalEventManager.OnPlayerEnterRoom += room.SpawnEnemies;

        //GlobalEventManager.OnPlayerLeaveRoom += room.SpawnRoom; //need some thinking

        GlobalEventManager.OnRoomCleared += room.LiftBarriers;
        //GlobalEventManager.OnRoomCleared += GameManager.instance.CountClearedRooms;
        GlobalEventManager.OnRoomCleared += GameManager.instance.PostRoomCleared;
        GlobalEventManager.OnEnemyDied += room.EnemyDied;//something here prob count dead units track in level mngr
        
    }
    //on each roomspawn
    public override void UnsubscribeToEvents(Room room)
    {

    }
    public override void SubscribeOnSpawnRoom(Room room)
    {

    }
    public override void UnSubscribeOnSpawnRoom(Room room)
    {

    }
}
