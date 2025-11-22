/*

 
 
 
 
 
 */

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

using NUnit.Framework;

using Unity.Mathematics;

using UnityEngine;

public enum Direction {LEFT, RIGHT, UP, DOWN}
public class RoomManager : Singleton<RoomManager> 
{
    public static new RoomManager instance =>Singleton<RoomManager>.instance;
    public Room roomPrefab;
    public List<Room> roomPrefabList = new List<Room>();
    public List<Room> roomList = new List<Room>();
    public float roomSize;
    public int roomGridSize=200;
    public Room[,] roomGrid;
    // first value is Y second X
    public Room startTile;
    class NeighborReference
    {
        string neighborStatus;
    }
    private void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        
        SetupGrid();
    }
    private void SetupGrid()
    {
        roomGrid = new Room[roomGridSize, roomGridSize];
        int centreCoord = roomGridSize / 2;
        roomGrid[centreCoord, centreCoord] = startTile;
        startTile.xPosInArray = centreCoord;
        startTile.yPosInArray = centreCoord;
    }
    /*
     spawning logic
    //check from which direction we are comming
    //get x/y pos of tile we are spawning
    //check neighboring tiles of one we will spawn
    //check if there is any, if yes check if its open towards our tile
    //based on that get code for compatible shapes
    //spawn room
    //disable triggers of neighboring tiles w doors to new room
    //disable triggers of spawned tile
     */
    public void SpawnRoom(Room inputRoom, Direction direction)
    {
        Debug.Log("SpawningRoom");
        //check from which direction we are comming from - we know from inputdirection
        //get x/y pos of tile we are spawning
        int xOfSpawn;
        int yOfSpawn;
        GetCoordsOfSpawnedRoom(out xOfSpawn,out yOfSpawn,inputRoom,direction);
        Debug.Log("We Have coords of new room");
        //Debug.Log("Coords of spawned room are x: "+ xOfSpawn+" y: "+yOfSpawn);
        //check neighboring tiles of one we will spawn, //put neighbors into some collection???
        if (roomGrid[yOfSpawn, xOfSpawn])
        {
            Debug.Log("SlotAlreadyUsed");
            return;
        }

        Room neighborLeft;
        Room neighborRight;
        Room neighborTop;
        Room neighborBottom;

        
        { }
        List<Room>neighborList = new List<Room>();
        
        //rooms set above
        GetRoomByCoord(xOfSpawn - 1, yOfSpawn, out neighborLeft);

        GetRoomByCoord(xOfSpawn + 1, yOfSpawn,out neighborRight);
        GetRoomByCoord(xOfSpawn, yOfSpawn + 1, out neighborTop);
        GetRoomByCoord(xOfSpawn, yOfSpawn - 1, out neighborBottom);
        Debug.Log("We know our neighbors");

        // make list of viable Rooms to Spawn
        List<Room> returnList = new List<Room>();
        //returnList = roomPrefabList;
        foreach(Room room in roomPrefabList) returnList.Add(room);

        Debug.Log("Number of returnList rooms: "+ returnList.Count);
        List<Room> removeList = new List<Room>();
        if (!(neighborLeft == null))
        {
            neighborList.Add(neighborLeft);
            foreach (Room room in returnList) 
            {
                if (neighborLeft.rightDoor!=room.leftDoor)
                {
                    //returnList.Remove(room);
                    removeList.Add(room);
                }
            }
        }
        if (!(neighborRight == null))
        {
            neighborList.Add(neighborRight);
            foreach (Room room in returnList)
            {
                if (neighborRight.leftDoor != room.rightDoor) 
                {
                    //returnList.Remove(room);
                    removeList.Add(room);
                }
            }
        }
        if (!(neighborTop == null))
        {
            neighborList.Add(neighborRight);
            foreach (Room room in returnList)
            {
                if (neighborTop.topDoor != room.bottomDoor) 
                {
                    //returnList.Remove(room);
                    removeList.Add(room);
                }
            }
        }
        if (!(neighborBottom == null))
        {
            neighborList.Add(neighborRight);
            foreach (Room room in returnList)
            {
                if (neighborBottom.bottomDoor != room.topDoor) 
                { 
                    //returnList.Remove(room);
                    removeList.Add(room);
                }
            }
        }
        foreach (Room room in removeList)
        { 
            returnList.Remove(room); 
        }
        Debug.Log("NUmberOfNeghbors: " + neighborList.Count);
        foreach ( Room room in neighborList)
        { 
            Debug.Log(nameof(room));
        }

        int randomRoomIndex = UnityEngine.Random.Range(0,returnList.Count-1);
        Debug.Log("Number of viable rooms: " + returnList.Count);
        Room pickedRoomPrefab = returnList[randomRoomIndex];

        Vector3 spawnPosition = this.transform.position;
        //we need to check what is direction we came from to spawn tile

        //spawn room
        spawnPosition = inputRoom.transform.position + ReturnRoomSpawnPosition(inputRoom,direction);
        //Room spawnedRoom = Instantiate(roomPrefab,spawnPosition,Quaternion.identity);
        Room spawnedRoom = Instantiate(pickedRoomPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("SpawningRoom");
        AddRoomToArray(spawnedRoom,inputRoom, direction);
        //disable triggers of neighboring tiles  to new room //w doors???
        //disable triggers of spawned tile
        try
        { 
            DisableTriggerIfNotNull(neighborLeft, neighborLeft.DisableRightTrigger, spawnedRoom.DisableLeftTrigger); 
        } catch 
        { Debug.Log("no left neighbor"); };
        try 
        { 
            DisableTriggerIfNotNull(neighborRight, neighborRight.DisableLeftTrigger, spawnedRoom.DisableRightTrigger); 
        } catch 
        { Debug.Log("no right neighbor"); };
        try 
        { 
            DisableTriggerIfNotNull(neighborTop, neighborTop.DisableBottomTrigger, spawnedRoom.DisableTopTrigger); 
        } catch 
        { Debug.Log("no top neighbor"); };
        try 
        { 
            DisableTriggerIfNotNull(neighborBottom, neighborBottom.DisableTopTrigger, spawnedRoom.DisableBottomTrigger); 
        } catch 
        { Debug.Log("no bottom neighbor"); };

        roomList.Add(spawnedRoom);
    }
    private void DoNeighborStuffLeft(Room room) 
    {
        bool door = room.rightDoor;
        room.DisableRightTrigger();
    }
    private void DoNeighborStuffRight(Room room)
    {
        bool door = room.leftDoor;
        room.DisableLeftTrigger();
    }
    private void DoNeighborStuffTop(Room room)
    {
        bool door = room.bottomDoor;
        room.DisableBottomTrigger();
    }
    private void DoNeighborTopfBottom(Room room)
    {
        bool door = room.topDoor;
        room.DisableTopTrigger();
    }

    private void CheckNeighbors(int x, int y)
    {
        bool left= IsGridSlotUsedBool(x-1,y);
        bool right = IsGridSlotUsedBool(x+1,y);
        bool top = IsGridSlotUsedBool(x,y+1);
        bool bottom = IsGridSlotUsedBool(x,y-1);

        string spawnString = top.ToString() + left.ToString() + bottom.ToString()+right.ToString();
        Debug.Log(spawnString);
    }

    private bool IsGridSlotUsedBool(int x, int y)
    {
        if (roomGrid[y, x] == null)
            return false;
        else 
            return true;

    }
    private int IsGridSlotUsedInt(int x, int y)
    {
        if (roomGrid[y, x] == null)
            return 0;
        else
            return 1;
    }
    private void GetRoomByCoord(int x, int y,out Room room)
    {
        room = roomGrid[y, x];
    }
    private void MakeListOfViableRooms()
    {//prolly remove
        List<Room> returnList = new List<Room>();
        returnList = roomPrefabList;
        
    }
    /*private void IsDoorThere(Room room,bool inputIsDoor,out bool isDoor)
    { 
        //if(!(room ==null)) inputIsDoor
    }*/
    private void DisableTriggerIfNotNull(Room room, Action action1,Action action2)
    {
        if (!(room == null)) { action1(); action2(); }
        else { Debug.Log("DontHaveNeighbor"); }
    }
    
    private Vector3 ReturnRoomSpawnPosition(Room room,Direction direction)
    {
        Vector3 position = this.transform.position;
        switch (direction)
        {
            case Direction.LEFT:{position = new Vector3(-roomSize,0,0);}break;
            case Direction.RIGHT:{position = new Vector3(roomSize, 0, 0);}break;
            case Direction.UP:{position = new Vector3(0,roomSize, 0);}break;
            case Direction.DOWN:{position = new Vector3(0, -roomSize, 0);}break;
            default:{Debug.Log("Unknown direction");break;}
        }
        return position;
    }
    private void AddRoomToArray(Room addedRoom, Room roomWeCameFrom,Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                {
                    roomGrid[roomWeCameFrom.yPosInArray, roomWeCameFrom.xPosInArray - 1] = roomWeCameFrom;
                    addedRoom.xPosInArray = roomWeCameFrom.xPosInArray - 1;
                    addedRoom.yPosInArray = roomWeCameFrom.yPosInArray;
                }
                break;
            case Direction.RIGHT:
                {
                    roomGrid[roomWeCameFrom.yPosInArray, roomWeCameFrom.xPosInArray + 1] = roomWeCameFrom;
                    addedRoom.xPosInArray = roomWeCameFrom.xPosInArray + 1;
                    addedRoom.yPosInArray = roomWeCameFrom.yPosInArray;
                }
                break;
            case Direction.UP:
                {
                    roomGrid[roomWeCameFrom.yPosInArray+1, roomWeCameFrom.xPosInArray] = roomWeCameFrom;
                    roomGrid[roomWeCameFrom.yPosInArray, roomWeCameFrom.xPosInArray + 1] = roomWeCameFrom;
                    addedRoom.xPosInArray = roomWeCameFrom.xPosInArray;
                    addedRoom.yPosInArray = roomWeCameFrom.yPosInArray + 1;
                }
                break;
            case Direction.DOWN:
                {
                    roomGrid[roomWeCameFrom.yPosInArray-1, roomWeCameFrom.xPosInArray] = roomWeCameFrom;
                    addedRoom.xPosInArray = roomWeCameFrom.xPosInArray;
                    addedRoom.yPosInArray = roomWeCameFrom.yPosInArray - 1;
                }
                break;
            default:
                {
                    Debug.Log("Unknown direction");
                    break;
                }
        }
    }

    private void GetCoordsOfSpawnedRoom(out int x,out int y,Room roomWeCameFrom, Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                {
                    x = roomWeCameFrom.xPosInArray - 1;
                    y = roomWeCameFrom.yPosInArray;
                }
                break;
            case Direction.RIGHT:
                {
                    x = roomWeCameFrom.xPosInArray + 1;
                    y = roomWeCameFrom.yPosInArray;
                }
                break;
            case Direction.UP:
                {
                    x = roomWeCameFrom.xPosInArray;
                    y = roomWeCameFrom.yPosInArray + 1;
                }
                break;
            case Direction.DOWN:
                {
                    x = roomWeCameFrom.xPosInArray;
                    y = roomWeCameFrom.yPosInArray - 1;
                }
                break;
            default:
                {
                    Debug.Log("Unknown direction");
                    x = 999999;
                    y = 999999;
                    break;
                }
        }
    }
}
