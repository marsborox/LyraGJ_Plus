using UnityEngine;

using static GlobalEventManager;

[CreateAssetMenu(fileName = "Level_SO", menuName = "Scriptable Objects/Level_SO")]
public class Level_SO : ScriptableObject
{
    public virtual void SubscribeToEvents(Room room)
    { 
    
    }
    //on each roomspawn


    //public static RoomEvent OnPlayerEnterRoom;
    //public static RoomEvent OnPlayerLeaveRoom;
    //public static RoomEvent OnRoomCleared;
    //public static EnemyEvent OnEnemyDied;
}
