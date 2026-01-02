using System;
using System.Collections.Generic;

using NavMeshPlus.Components;

using UnityEngine;

public enum Direction {LEFT, RIGHT, UP, DOWN}
public class RoomManager : Singleton<RoomManager> 
{
    public static new RoomManager instance =>Singleton<RoomManager>.instance;


    public Room roomPrefab;
    public Room[,] roomGrid;
    // first value is Y second X
    public Room startTile;
    public List<Room> roomPrefabList = new List<Room>();
    public List<Room> deadEndPrefabList = new List<Room>();
    public List<Room> roomList = new List<Room>();
    public int roomSize=20;
    public int roomGridSize=200;
    public int spawnedRoomCount;
    public int maxSpawnedRoomCount;

    
    [SerializeField] private RoomSpawner _roomSpawner;
    [SerializeField] private RoomObjectSpawner _roomObjectSpawner;
    private int _spawnedRoomCounter = 0;

    public NavMeshSurface surface;
    private void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        SetupGridAndStartRoom();
        GameManager.instance.levelSettings.SubscribeToEventsSingletons(startTile);
        GameManager.instance.levelSettings.StartRoomSetup(startTile);
        //GameManager.instance.levelSettings.SubscribeToEventsRoom(startTile);
    }
    private void SetupGridAndStartRoom()
    {
        roomGrid = new Room[roomGridSize, roomGridSize];
        int centreCoord = roomGridSize / 2;
        roomGrid[centreCoord, centreCoord] = startTile;
        startTile.xPosInArray = centreCoord;
        startTile.yPosInArray = centreCoord;
        _spawnedRoomCounter++;
    }

    public void SpawnRoom(Room inputRoom, Direction direction)
    {
        spawnedRoomCount++;
        //Debug.Log("should spawn room, spawnedRoomCount = "+spawnedRoomCount);
        if (spawnedRoomCount == maxSpawnedRoomCount)
        {
            //Debug.Log("should spawn finalRoom");
            SpawnFinalRoom(inputRoom, direction);
            
        }
        else
        {
            //Debug.Log("should spawn normalRoom");
            Room spawnedRoom = _roomSpawner.SpawnRoom(inputRoom, direction, roomPrefabList, deadEndPrefabList, ref roomList, roomGrid, roomSize, ref _spawnedRoomCounter);
            GameManager.instance.levelSettings.SubscribeToEventsRoom(spawnedRoom);
            surface.BuildNavMesh();
        }
    }
    
    public void TestArrayContent()
    {
        _roomSpawner.TestArrayContent(roomGrid,roomGridSize);
    }
    public void SpawnFinalRoom(Room inputRoom, Direction direction)
    {
        Debug.Log("Spawqning final room");
        Room spawnedRoom = _roomSpawner.SpawnRoom(inputRoom, direction, roomPrefabList, deadEndPrefabList, ref roomList, roomGrid, roomSize, ref _spawnedRoomCounter);
        GameManager.instance.levelSettings.SubscribeToEventsRoom(spawnedRoom);
        surface.BuildNavMesh();
        _roomObjectSpawner.SpawnPortal(spawnedRoom);
    }
}
