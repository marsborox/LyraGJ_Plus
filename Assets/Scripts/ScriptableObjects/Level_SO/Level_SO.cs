using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

//[CreateAssetMenu(fileName = "Level_SO", menuName = "Scriptable Objects/Level_SO")]
public class Level_SO : ScriptableObject
{
    //public List<int> spawnDiaogueOnRoomIndexList = new List<int>();
    public int numberOfRoomsToClear = 10;
    public List<DialogueToIndex> dialogueWRoomClearedIndexList = new List<DialogueToIndex>();
    [System.Serializable]
    public class DialogueToIndex
    {
        public int spawnOnRoomCleared;
        public Dialogue_SO dialogue;
    }

    public int minEnemiesPerRoom;
    public int maxEnemiesPerRoom;
    public SpriteLibraryAsset lyraVisual;
    public virtual void SubscribeToEventsSingletons(Room room)
    { }
    public virtual void StartRoomSetup(Room room)
    { }
    public virtual void SubscribeToEventsRoom(Room room)
    { }
    public virtual void UnsubscribeToEventsRoom(Room room)
    { }
    //on each roomspawn
    public virtual void UnsubscribeToEvents(Room room) 
    { }
    public virtual void SubscribeOnSpawnRoom(Room room)
    { }
    public virtual void UnSubscribeOnSpawnRoom(Room room)
    { }
    public virtual void PlayerDied()
    { }
    public virtual void PostLevelClear()
    {}
    //public static RoomEvent OnPlayerEnterRoom;
    //public static RoomEvent OnPlayerLeaveRoom;
    //public static RoomEvent OnRoomCleared;
    //public static EnemyEvent OnEnemyDied;
}
