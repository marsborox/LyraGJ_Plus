using UnityEngine;

[CreateAssetMenu(fileName = "TutorialLevel_SO", menuName = "Scriptable Objects/Level_SOs/TutorialLevel_SO")]
public class TutorialLevel_SO : Level_SO
{
    [SerializeField] private Enemy_SO _redEnemy;
    [SerializeField] private Enemy_SO _greenEnemy;
    [SerializeField] private Enemy_SO _blueEnemy;
    public override void SubscribeToEventsSingletons(Room room)
    {
        GlobalEventManager.OnRoomCleared += GameManager.instance.PostRoomCleared;
    }
    public override void SubscribeToEventsRoom(Room room)
    {
        GlobalEventManager.OnPlayerEnterRoom += room.SpawnEnemies;
        //GlobalEventManager.OnPlayerLeaveRoom += room.SpawnRoom; //need some thinking
        GlobalEventManager.OnRoomCleared += room.LiftBarriers;
        //GlobalEventManager.OnRoomCleared += GameManager.instance.CountClearedRooms;
        GlobalEventManager.OnEnemyDied += room.EnemyDied;//something here prob count dead units track in level mngr
    }

    public void SpawnTutorialRoom(Room room)
    {
        int roomsCleared = GameManager.instance.roomsCleared;

        if (roomsCleared == 0) 
        {
            
        }
        else if (roomsCleared == 1)
        {
            
        }
        else if (roomsCleared == 2)
        {
            
        }
        else if (roomsCleared == 3)
        {
            
        }
        else 
        {
            Debug.Log("roomNotImplemented");
        }

        switch (roomsCleared)
        { 
            case 0: 
                {
                    
                    break; 
                }

            case 1: 
                {
                    UnitSpawner.instance.SpawnEnemy(room, _redEnemy);
                    break;
                }
            case 2: 
                {
                    UnitSpawner.instance.SpawnEnemy(room, _greenEnemy);
                    break; 
                }
            case 3: 
                {
                    UnitSpawner.instance.SpawnEnemy(room, _blueEnemy);
                    break; 
                }
        }
    }
    public void SpawnRedEnemy()
    { }
    public void SpawnGreenEnemy() 
    { }
    public void SpawnBlueEnemy() 
    { }
}
