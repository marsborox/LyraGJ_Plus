using UnityEngine;

[CreateAssetMenu(fileName = "TutorialLevel_SO", menuName = "Scriptable Objects/Level_SOs/TutorialLevel_SO")]
public class TutorialLevel_SO : Level_SO
{
    [SerializeField] private Enemy_SO _redEnemy;
    [SerializeField] private Enemy_SO _greenEnemy;
    [SerializeField] private Enemy_SO _blueEnemy;
    public override void SubscribeToEventsSingletons(Room room)
    {
        GlobalEventManager.OnRoomCleared += GameManager.instance.SpawnDialogue;
        GlobalEventManager.OnPlayerEnterRoom += SpawnTutorialRoom;
    }
    public override void StartRoomSetup(Room room)
    {
        Debug.Log("startroomsetup room: "+room.roomID.ToString());
        GlobalEventManager.OnPlayerEnterRoom += room.LiftBarriers;
        GlobalEventManager.OnPlayerEnterRoom += GameManager.instance.SpawnDialogue;
        GlobalEventManager.OnPlayerLeaveRoom += GameManager.instance.ForceRoomCleared;
        //GlobalEventManager.OnPlayerLeaveRoom += GameManager.ForceRoomCleared;

        GlobalEventManager.OnPlayerLeaveRoom += SingleUnSubscribe;
    }
    public override void SubscribeToEventsRoom(Room room)
    {
        //GlobalEventManager.OnPlayerEnterRoom += SpawnTutorialRoom;
        //GlobalEventManager.OnPlayerLeaveRoom += room.SpawnRoom; //need some thinking
        GlobalEventManager.OnEnemyDied += room.EnemyDied;//something here prob count dead units track in level mngr
        GlobalEventManager.OnRoomCleared += room.LiftBarriers;
        //GlobalEventManager.OnRoomCleared += GameManager.instance.CountClearedRooms;
        
    }
    public void SingleUnSubscribe(Room room)
    {
        GlobalEventManager.OnPlayerLeaveRoom -= GameManager.instance.ForceRoomCleared;
        GlobalEventManager.OnPlayerEnterRoom -= GameManager.instance.SpawnDialogue;
    }

    public void SpawnTutorialRoom(Room room)
    {
        int roomsCleared = GameManager.instance.roomsCleared;
        // int roomsSpawned = GameManager.
        //Debug.Log("tutorial room spawn triggered");
        switch (roomsCleared)
        { 
            case 0: 
                {
                    
                    break; 
                }

            case 1: 
                {
                    room.SpawnParticularEnemy(room, _redEnemy);
                    break;
                }
            case 2: 
                {
                    room.SpawnParticularEnemy(room, _greenEnemy);
                    break; 
                }
            case 3: 
                {
                    room.SpawnParticularEnemy(room, _blueEnemy);
                    break; 
                }
            case 4:
                {
                    room.SpawnParticularEnemy(room, _redEnemy);
                    room.SpawnParticularEnemy(room, _greenEnemy);
                    room.SpawnParticularEnemy(room, _blueEnemy);
                    break;
                }
                case 5:
                {
                    room.SpawnSomeEnemiesRandomly(room);
                    break;
                }
            default:
                {

                    break;
                }
        }
    }

}
