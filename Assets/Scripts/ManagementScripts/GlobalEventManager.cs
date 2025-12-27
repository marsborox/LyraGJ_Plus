using UnityEngine;

public class GlobalEventManager : Singleton<GlobalEventManager>
{
    public static new GlobalEventManager instance => Singleton<GlobalEventManager>.instance;

    public delegate void RoomEvent(Room room);
    public delegate void EnemyEvent(Enemy enemy,Room room);

    public static RoomEvent OnPlayerEnterRoom;
    public static RoomEvent OnPlayerLeaveRoom;
    public static RoomEvent OnRoomCleared;
    public static EnemyEvent OnEnemyDied;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerOnPlayerEnterRoom(Room room)
    {
        //triggered in spawnTrigger
        OnPlayerEnterRoom?.Invoke(room);
    }
    public void TriggerOnPlayerLeaveRoom(Room room,EntryTrigger trigger) 
    {   //triggered in room TriggerActivated  
        OnPlayerLeaveRoom?.Invoke(room);
    }
    public void TriggerOnRoomCleared(Room room)
    { 
        //triggered in Room EnemyDied part where we register room cleared
        OnRoomCleared?.Invoke(room);
    }
    public void TriggerEnemyDied(Enemy enemy,Room room)
    { 
        //triggered in EnemyCombat Die
        OnEnemyDied?.Invoke(enemy, room);
    }
}
