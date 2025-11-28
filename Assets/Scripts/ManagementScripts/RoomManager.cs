using System;
using System.Collections.Generic;

using NavMeshPlus.Components;

using UnityEngine;

public enum Direction {LEFT, RIGHT, UP, DOWN}
public class RoomManager : Singleton<RoomManager> 
{
    public static new RoomManager instance =>Singleton<RoomManager>.instance;
    public Room roomPrefab;
    public List<Room> roomPrefabList = new List<Room>();
    public List<Room> deadEndPrefabList = new List<Room>();
    public List<Room> roomList = new List<Room>();
    public int roomSize=20;
    public int roomGridSize=200;
    public Room[,] roomGrid;
    // first value is Y second X
    public Room startTile;

    [SerializeField]private RoomSpawner _roomSpawner;
    private int _spawnedRoomCounter = 0;

    public NavMeshSurface surface;
    private void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        SetupGridAndStartRoom();
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
        _roomSpawner.SpawnRoom(inputRoom,direction,roomPrefabList, deadEndPrefabList,ref roomList,roomGrid,roomSize,ref _spawnedRoomCounter);
        surface.BuildNavMesh();
    }
    
    public void TestArrayContent()
    {
        _roomSpawner.TestArrayContent(roomGrid,roomGridSize);
    }

}
