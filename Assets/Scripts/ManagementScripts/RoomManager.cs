/*

 
 
 
 
 
 */

using System.Collections.Generic;

using NUnit.Framework;

using Unity.Mathematics;

using UnityEngine;

public enum Direction {LEFT, RIGHT, UP, DOWN}
public class RoomManager : Singleton<RoomManager> 
{
    public static new RoomManager instance =>Singleton<RoomManager>.instance;
    public Room roomPrefab;
    public List<Room> roomList = new List<Room>();
    public float roomSize;
    public int roomGridSize=200;
    public Room[,] roomGrid;
    // first value is Y second X
    public Room startTile;
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

    public void SpawnRoom(Room inputRoom, Direction direction)
    {
        Vector3 position = this.transform.position;


        position = inputRoom.transform.position + ReturnRoomSpawn(inputRoom,direction);
        Room spawnedRoom = Instantiate(roomPrefab,position,Quaternion.identity);
        AddRoomToArray(spawnedRoom,inputRoom, direction);

        roomList.Add(spawnedRoom);
    }
    void CheckNeighbors(int x, int y)
    {
        bool left= IsGridSlotUsed(x-1,y);
        bool right= IsGridSlotUsed(x+1, y);
        bool top = IsGridSlotUsed(x,y+1);
        bool bottom = IsGridSlotUsed(x,y-1);

        string spawnString = top.ToString() + left.ToString() + bottom.ToString()+right.ToString();
        Debug.Log(spawnString);
    }
    bool IsGridSlotUsed(int x, int y)
    {
        if (roomGrid[y, x] = null)
            return true;
        else 
            return false;

    }


    private Vector3 ReturnRoomSpawn(Room room,Direction direction)
    {
        Vector3 position = this.transform.position;
        switch (direction)
        {
            case Direction.LEFT:
                {
                    position = new Vector3(-roomSize,0,0);

                }
                break;
            case Direction.RIGHT:
                {
                    position = new Vector3(roomSize, 0, 0);
                }
                break;
            case Direction.UP:
                {
                    position = new Vector3(0,roomSize, 0);
                }
                break;
            case Direction.DOWN:
                {
                    position = new Vector3(0, -roomSize, 0);
                }
                break;
            default:
                {
                    Debug.Log("Unknown direction");
                    break;
                }
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
}
