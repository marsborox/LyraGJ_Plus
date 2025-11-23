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

    private int spawnedRoomCounter = 0;
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
        spawnedRoomCounter++;
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
        Debug.Log("-------------------------------- NEW SPAWN ------------------------------------");
        Debug.Log("SpawningRoom");
        //check from which direction we are comming from - we know from inputdirection
        //get x/y pos of tile we are spawning
        int xOfSpawn;
        int yOfSpawn;
        GetCoordsOfSpawnedRoom(out xOfSpawn,out yOfSpawn,inputRoom,direction);
        Debug.Log("We Have coords of new inputRoom");
        //Debug.Log("Coords of spawned room are x: "+ xOfSpawn+" y: "+yOfSpawn);
        //check neighboring tiles of one we will spawn, //put neighbors into some collection???
        Debug.Log("spawning removeRoom at x: "+xOfSpawn + "y: "+yOfSpawn);
        if (roomGrid[yOfSpawn, xOfSpawn])
        {
            Debug.Log("SlotAlreadyUsed");
            return;
        }

        Room neighborLeft;
        Room neighborRight;
        Room neighborTop;
        Room neighborBottom;

        List<Room>neighborList = new List<Room>();
        
        //rooms set above
        GetRoomByCoord(xOfSpawn - 1, yOfSpawn, out neighborLeft);
        GetRoomByCoord(xOfSpawn + 1, yOfSpawn,out neighborRight);
        GetRoomByCoord(xOfSpawn, yOfSpawn + 1, out neighborTop);
        GetRoomByCoord(xOfSpawn, yOfSpawn - 1, out neighborBottom);
        //Debug.Log("We know our neighbors");

        // make list of viable Rooms to Spawn
        List<Room> returnList = new List<Room>();
        //returnList = roomPrefabList;
        //foreach(Room room in roomPrefabList) returnList.Add(room);


        Debug.Log("Number of returnList rooms: "+ returnList.Count);
        List<Room> removeList = new List<Room>();
        Debug.Log("-----------------------Removing Rooms from potential list----------------------------");
        if (!(neighborLeft == null))
        {
            neighborList.Add(neighborLeft);
            foreach (Room room in roomPrefabList) 
            {
                if (neighborLeft.rightDoor!=room.leftDoor)
                {
                    //returnList.Remove(room);
                    //removeList.Add(room);
                    AddRoomPrefabToList(room,ref removeList);
                    Debug.Log(room.name + " removed because wrong left");
                }
            }
        }
        if (!(neighborRight == null))
        {
            neighborList.Add(neighborRight);
            foreach (Room room in roomPrefabList)
            {
                //Debug.Log("Right Neighbors door: "+neighborRight.leftDoor.ToString() + " checked left removeRoom door: "+room.rightDoor.ToString());
                
                if (neighborRight.leftDoor != room.rightDoor) 
                {
                    //returnList.Remove(room);
                    //removeList.Add(room);
                    AddRoomPrefabToList(room, ref removeList);
                    Debug.Log(room.name + " removed because wrong right");
                }
            }
        }
        if (!(neighborTop == null))
        {
            neighborList.Add(neighborTop);
            foreach (Room room in roomPrefabList)
            {

                if (neighborTop.bottomDoor != room.topDoor) 
                {
                    //returnList.Remove(room);
                    //removeList.Add(room);
                    AddRoomPrefabToList(room, ref removeList);
                    Debug.Log(room.name + " removed because wrong top");
                }
            }
        }
        if (!(neighborBottom == null))
        {
            neighborList.Add(neighborBottom);
            
            foreach (Room room in roomPrefabList)
            {
                if (neighborBottom.topDoor != room.bottomDoor) 
                { 
                    //returnList.Remove(room);
                    //removeList.Add(room);
                    AddRoomPrefabToList(room, ref removeList);
                    Debug.Log(room.name + " removed because wrong bottom");
                }
            }
        }
        //scan over non compatible prefabs if our is not in that list we can use it
        //returnList.Clear();//clean this remove above and this
        foreach (Room roomPrefab in roomPrefabList)
        {//not sure if works 
            if (removeList.Count==0)
            {
                returnList.Add(roomPrefab);
                Debug.Log("RemoveList is Empty");
            }
            else
            {
                bool isRemove = false;
                foreach (Room removeRoom in removeList)
                {

                    //returnList.Remove(roomPrefab);
                    if (roomPrefab.name == removeRoom.name)
                    {
                        isRemove = true;
                    }
                }
                if(!isRemove)returnList.Add(roomPrefab);
            }
        }
        Debug.Log("NumberOfNeghbors: " + neighborList.Count);
        /*foreach ( Room room in neighborList)
        { 
            Debug.Log(nameof(room));
        }*/
        
        int randomRoomIndex = UnityEngine.Random.Range(0,returnList.Count-1);
        Debug.Log("not viable roomPrefabs: "+ removeList.Count);
        Debug.Log("Number of viable roomPrefabs: " + returnList.Count);

        PrintAllRoomNamesInList(removeList,nameof(removeList));
        PrintAllRoomNamesInList(returnList, nameof(returnList));
        //******************************************************************************
        //tuna nejaky if returnlistcount 0 - podla smeru vyber dead end else
        Room pickedRoomPrefab = returnList[randomRoomIndex];

        Vector3 spawnPosition = this.transform.position;
        //we need to check what is direction we came from to spawn tile

        //spawn room
        spawnPosition = inputRoom.transform.position + ReturnRoomSpawnPosition(inputRoom,direction);
        //Room spawnedRoom = Instantiate(roomPrefab,spawnPosition,Quaternion.identity);
        Room spawnedRoom = Instantiate(pickedRoomPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("SpawningRoom");
        roomGrid[yOfSpawn,xOfSpawn]=spawnedRoom;
        spawnedRoom.xPosInArray = xOfSpawn;
        spawnedRoom.yPosInArray = yOfSpawn;
        spawnedRoom.roomID = spawnedRoomCounter;
        spawnedRoomCounter++;
        //AddRoomToArray(spawnedRoom,inputRoom, direction);
        //disable triggers of neighboring tiles  to new room //w doors???
        //disable triggers of spawned tile
        try
        { 
            DisableTriggersIfNotNull(neighborLeft, neighborLeft.DisableRightTrigger, spawnedRoom.DisableLeftTrigger); 
        } catch 
        { Debug.Log("no left neighbor"); };
        try 
        { 
            DisableTriggersIfNotNull(neighborRight, neighborRight.DisableLeftTrigger, spawnedRoom.DisableRightTrigger); 
        } catch 
        { Debug.Log("no right neighbor"); };
        try 
        { 
            DisableTriggersIfNotNull(neighborTop, neighborTop.DisableBottomTrigger, spawnedRoom.DisableTopTrigger); 
        } catch 
        { Debug.Log("no top neighbor"); };
        try 
        { 
            DisableTriggersIfNotNull(neighborBottom, neighborBottom.DisableTopTrigger, spawnedRoom.DisableBottomTrigger); 
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
    public void AddRoomPrefabToList(Room inputRoom, ref List<Room>list)
    {
        if (list.Count == 0)
        {
            list.Add(inputRoom);
            //Debug.Log("provided list was empty adding removeRoom to list");
        }
        else
        {
            bool wasUsed = false;
            foreach (Room listRoom in list)
            {
                if (listRoom.roomName == inputRoom.roomName)
                {
                    wasUsed = true;
                }

            }
            if (!wasUsed)
            {
                list.Add(inputRoom);
                //Debug.Log("adding removeRoom to list");
            }
        }
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
    private void DisableTriggersIfNotNull(Room room, Action action1,Action action2)
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
    public void TestArrayContent()
    {
        int ammountOfNull=0;
        int ammountOfUsed = 0;
        Debug.Log("--------------------------TESTING ARRAY --------------------");
        for (int y = 0; y < roomGridSize; y++)
        {
            for (int x = 0; x < roomGridSize; x++) 
            {
                if (roomGrid[y, x] != null)
                {
                    Room room = roomGrid[y, x];
                    string resultText;
                    ammountOfUsed++;
                    if (room.yPosInArray == y && room.xPosInArray == x)
                    {
                        resultText = "data correct";
                    }
                    else
                    {
                        resultText = "data NOT correct";
                        Debug.Log("expected X: "+x +" Y: "+y);
                        Debug.Log("delivered X: "+ room.xPosInArray + " Y: "+ room.yPosInArray);
                    }

                    Debug.Log("removeRoom ID: " + room.roomID + " " + resultText);
                }
                else 
                {
                    ammountOfNull++;
                }
            }
        }
        Debug.Log("gridslots null: "+ ammountOfNull + "gridslots used: "+ammountOfUsed);
    }
    public void PrintAllRoomNamesInList(List<Room> roomList,string listName)
    {
        foreach (Room room in roomList)
        {
            Debug.Log("ListName: "+ listName + " RoomName: "+room.name);
        }
    }
}
/*private void AddRoomToArray(Room addedRoom, Room roomWeCameFrom,Direction direction)
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
}*/
