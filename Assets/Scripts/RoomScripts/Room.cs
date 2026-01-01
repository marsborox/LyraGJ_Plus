using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Room : MonoBehaviour
{
    public bool leftDoor;
    public bool rightDoor;
    public bool topDoor;
    public bool bottomDoor;
    public string roomName;

    [SerializeField] private EntryTrigger _triggerLeft;
    [SerializeField] private EntryTrigger _triggerRight;
    [SerializeField] private EntryTrigger _triggerTop;
    [SerializeField] private EntryTrigger _triggerBottom;
    [SerializeField] private SpawnTrigger _spawnTrigger;
    [SerializeField] private Transform _spawnAreaLT;
    [SerializeField] private Transform _spawnAreaRB;

    [SerializeField] private GameObject _barriers;

    [SerializeField] private bool isCleared = true;

    public List <EntryTrigger> entryTriggerList = new List<EntryTrigger>();
    public bool heroEntered = false;
    public bool enemiesSpawned = false;
    public int xPosInArray;
    public int yPosInArray;
    public int roomID;

    public int enemiesInRoomCount = 0;
    private void Start()
    {
        
    }
    public void TriggerActivated(EntryTrigger trigger)
    {
        /*if (!heroEntered)
        {
            HeroEntering(trigger);
        }
        else HeroLeaving(trigger);*/
        trigger.gameObject.SetActive(false);
        SpawnRoom(trigger);//subscribed over SO
        isCleared = false;
        GlobalEventManager.instance.TriggerOnPlayerLeaveRoom(this, trigger);
    }

    public void HeroEntering(EntryTrigger trigger)
    { 
        heroEntered = true;
        //Spawning/activating enemies
        //Debug.Log("hero Entered Room from " + trigger.name);
        //hero vosiel, spawn
        
    }
    public void SpawnRoom(EntryTrigger trigger)
    {
        //Debug.Log("hero leaved Room from " + trigger.name);
        //instantiating next room
        RoomManager.instance.SpawnRoom(this,trigger.direction);
    }

    public void DisableLeftTrigger()
    {
        _triggerLeft.gameObject.SetActive(false);
    }
    public void DisableRightTrigger()
    {
        _triggerRight.gameObject.SetActive(false);
    }
    public void DisableTopTrigger() 
    {
        _triggerTop.gameObject.SetActive(false);
    }
    public void DisableBottomTrigger() 
    {
        _triggerBottom.gameObject.SetActive(false);
    }


    public bool SpawnedNeighborFromLeft()
    {
        _triggerLeft.gameObject.SetActive(false);
        return leftDoor;
    }
    public bool SpawnedNeighborFromRight()
    {
        _triggerRight.gameObject.SetActive(false);
        return rightDoor;
    }
    public bool SpawnedNeighborFromTop()
    {
        _triggerTop.gameObject.SetActive(false);
        return topDoor;
    }
    public bool SpawnedNeighborFromBottom()
    {
        _triggerBottom.gameObject.SetActive(false);
        return bottomDoor;
    }
    public void SpawnEnemies(Room room)
    {
        if (room != this)
        {// to trigger only on our room
            //Debug.Log("notThisRoom ");
            return;
        }
        _spawnTrigger.gameObject.SetActive(false);
        _barriers.SetActive(true);   
        //Debug.Log("we shall spawn enemies");
        //TestSpawnSomeEnemies();
        SpawnEnemiesFromLevelSO();
    }
    public void SpawnEnemiesFromLevelSO()
    {
        int randomMin = GameManager.instance.levelSettings.minEnemiesPerRoom;
        int randomMax = GameManager.instance.levelSettings.maxEnemiesPerRoom;
        
        int spawnAmount = Random.Range(randomMin, randomMax);
        //Debug.Log("we know how many enemies: " + spawnAmount.ToString());
        for (int i = 0; i <= spawnAmount; i++)
        {
            //Debug.Log("spawning enemy");
            float spawnPosX = Random.Range(_spawnAreaRB.transform.position.x, _spawnAreaLT.transform.position.x);
            float spawnPosY = Random.Range(_spawnAreaRB.transform.position.y, _spawnAreaLT.transform.position.y);

            UnitSpawner.instance.SpawnRandomEnemy(spawnPosX, spawnPosY, this);
            enemiesInRoomCount++;
        }
    }
    public void SpawnSomeEnemiesRandomly()
    { //some random for testing
        int randomMin = 3;
        int randomMax = 5;
        int spawnAmount = Random.Range(randomMin, randomMax);
        //Debug.Log("we know how many enemies: " + spawnAmount.ToString());
        for (int i = 0; i<=spawnAmount; i++)
        {
            //Debug.Log("spawning enemy");
            float spawnPosX = Random.Range(_spawnAreaRB.transform.position.x, _spawnAreaLT.transform.position.x);
            float spawnPosY = Random.Range(_spawnAreaRB.transform.position.y, _spawnAreaLT.transform.position.y);

            UnitSpawner.instance.SpawnRandomEnemy(spawnPosX, spawnPosY,this);
            enemiesInRoomCount++;
        }
        
        //Debug.Log("spawning done");

    }

    public void SpawnXAmomountOfEnemies(int spawnAmount)
    { //some random for testing

        //Debug.Log("we know how many enemies: " + spawnAmount.ToString());
        for (int i = 0; i <= spawnAmount; i++)
        {
            //Debug.Log("spawning enemy");
            float spawnPosX = Random.Range(_spawnAreaRB.transform.position.x, _spawnAreaLT.transform.position.x);
            float spawnPosY = Random.Range(_spawnAreaRB.transform.position.y, _spawnAreaLT.transform.position.y);

            UnitSpawner.instance.SpawnRandomEnemy(spawnPosX, spawnPosY, this);
            enemiesInRoomCount++;
        }

        //Debug.Log("spawning done");

    }

    public void EnemyDied(Enemy enemy, Room room)
    {
        if (!isCleared)
        {
            enemiesInRoomCount--;
            if (enemiesInRoomCount == 0)
            {
                //do something
                //Debug.Log("__________________________________________");
                //Debug.Log("Room Clear");
                //LiftBarriers();
                isCleared = true;
                GameManager.instance.roomsCleared++;
                //DoPostRoom Stuff
                GlobalEventManager.instance.TriggerOnRoomCleared(this);
            }
        }
    }
    public void SpawnParticularEnemy(Room room, Enemy_SO enemySO)
    {
        if (room != this)
        {// to trigger only on our room
            Debug.Log("notThisRoom ");
            return;
        }
        UnitSpawner.instance.SpawnEnemy(room,enemySO);
    }
    public void ForceRoomCleared(Room room)
    {
        if (room != this)
        {// to trigger only on our room
            //Debug.Log("notThisRoom ");
            return;
        }
        GameManager.instance.roomsCleared++;
        GlobalEventManager.instance.TriggerOnRoomCleared(room);
    }
    public void LiftBarriers(Room room)
    {
        _barriers.SetActive(false);
    }
    
}
