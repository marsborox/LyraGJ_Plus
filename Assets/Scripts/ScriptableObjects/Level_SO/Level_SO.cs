using UnityEngine;

//[CreateAssetMenu(fileName = "Level_SO", menuName = "Scriptable Objects/Level_SO")]
public class Level_SO : ScriptableObject
{
    int minEnemiesPerRoom;
    int maxEnemiesPerRoom;
    public virtual void SubscribeToEvents(Room room)
    { 
    
    }
    //on each roomspawn
    public virtual void UnsubscribeToEvents(Room room) 
    { 
    
    }
    public virtual void SubscribeOnSpawnRoom(Room room)
    { 
        
    }
    public virtual void UnSubscribeOnSpawnRoom(Room room)
    {

    }
    //public static RoomEvent OnPlayerEnterRoom;
    //public static RoomEvent OnPlayerLeaveRoom;
    //public static RoomEvent OnRoomCleared;
    //public static EnemyEvent OnEnemyDied;
}
