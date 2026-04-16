using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    [SerializeField] public bool isCleared = false;

    public List <EntryTrigger> entryTriggerList = new List<EntryTrigger>();
    public bool heroEntered = false;
    public bool enemiesSpawned = false;
    public int xPosInArray;
    public int yPosInArray;
    public int roomID;

    public int enemiesInRoomCount = 0;
    private void Start()
    {
        if (isCleared) 
        {
            GlobalEventManager.instance.TriggerOnRoomCleared(this);
            
        }
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
        //isCleared = false;
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
    {//this is really used !!!!!!
        if (room != this || isCleared)
        {// to trigger only on our room
            //Debug.Log("notThisRoom ");
            return;
        }
        //isCleared = false;
        _spawnTrigger.gameObject.SetActive(false);
        _barriers.SetActive(true);   
        //Debug.Log("we shall spawn enemies");
        //TestSpawnSomeEnemies();
        SpawnEnemiesFromLevelSO();
    }
    public void SpawnEnemiesFromLevelSO()
    {
        //this is used
        int randomMin = GameManager.instance.levelSettings.minEnemiesPerRoom;
        int randomMax = GameManager.instance.levelSettings.maxEnemiesPerRoom;
        
        int spawnAmount = UnityEngine.Random.Range(randomMin, randomMax);
        //Debug.Log("we know how many enemies: " + spawnAmount.ToString());
        for (int i = 0; i <= spawnAmount; i++)
        {
            //Debug.Log("spawning enemy");
            float spawnPosX, spawnPosY;
            ReturnRandomPoint(out spawnPosX, out spawnPosY);

            UnitSpawner.instance.SpawnRandomEnemy(spawnPosX, spawnPosY, this);
            enemiesInRoomCount++;
        }
    }

    public void ReturnRandomPoint(out float spawnPosX, out float spawnPosY)
    {// implement where needed
        spawnPosX = UnityEngine.Random.Range(_spawnAreaRB.transform.position.x, _spawnAreaLT.transform.position.x);
        spawnPosY = UnityEngine.Random.Range(_spawnAreaRB.transform.position.y, _spawnAreaLT.transform.position.y);

        //Vector2 spawnPosition = new Vector2(spawnPosX,spawnPosY);
    }
    public void ReturnRandomPointRelative(out float spawnPosX, out float spawnPosY)
    {// implement where needed
        spawnPosX = UnityEngine.Random.Range(_spawnAreaRB.transform.localPosition.x, _spawnAreaLT.transform.localPosition.x);
        spawnPosY = UnityEngine.Random.Range(_spawnAreaRB.transform.localPosition.y, _spawnAreaLT.transform.localPosition.y);

        //Vector2 spawnPosition = new Vector2(spawnPosX,spawnPosY);
    }
    public bool IsWithinBounds(Vector2 position)
    {
        bool isInBounds=true;
        /*if(position.y >_spawnAreaLT.transform.localPosition.y || 
        position.y <_spawnAreaRB.transform.localPosition.y||
        position.x >_spawnAreaRB.transform.localPosition.x||
        position.x < _spawnAreaLT.transform.localPosition.x)*/
        if(position.y >_spawnAreaLT.transform.position.y || 
        position.y <_spawnAreaRB.transform.position.y||
        position.x >_spawnAreaRB.transform.position.x||
        position.x < _spawnAreaLT.transform.position.x)
        {isInBounds = false;}
        else{isInBounds = true;}
        Debug.Log("isInBounds: "+isInBounds);

        return isInBounds;
    }
    public void SpawnSomeEnemiesRandomly(Room room)
    { //some random for testing

        if (room != this|| isCleared)
        {// to trigger only on our room
            Debug.Log("notThisRoom ");
            return;
        }
        int randomMin = 3;
        int randomMax = 5;
        int spawnAmount = UnityEngine.Random.Range(randomMin, randomMax);
        //Debug.Log("we know how many enemies: " + spawnAmount.ToString());
        for (int i = 0; i<=spawnAmount; i++)
        {
            //Debug.Log("spawning enemy");
            float spawnPosX = UnityEngine.Random.Range(_spawnAreaRB.transform.position.x, _spawnAreaLT.transform.position.x);
            float spawnPosY = UnityEngine.Random.Range(_spawnAreaRB.transform.position.y, _spawnAreaLT.transform.position.y);

            UnitSpawner.instance.SpawnRandomEnemy(spawnPosX, spawnPosY,this);
            enemiesInRoomCount++;
        }
        _barriers.SetActive(true);
        //Debug.Log("spawning done");

    }

    public void SpawnXAmomountOfEnemies(int spawnAmount)
    { //some random for testing

        //Debug.Log("we know how many enemies: " + spawnAmount.ToString());
        for (int i = 0; i <= spawnAmount; i++)
        {
            //Debug.Log("spawning enemy");
            float spawnPosX = UnityEngine.Random.Range(_spawnAreaRB.transform.position.x, _spawnAreaLT.transform.position.x);
            float spawnPosY = UnityEngine.Random.Range(_spawnAreaRB.transform.position.y, _spawnAreaLT.transform.position.y);

            UnitSpawner.instance.SpawnRandomEnemy(spawnPosX, spawnPosY, this);
            enemiesInRoomCount++;
        }

        //Debug.Log("spawning done");

    }
    public void SpawnParticularEnemy(Room room, Enemy_SO enemySO)
    {
        
        if (room != this|| isCleared)
        {// to trigger only on our room
            //Debug.Log("notThisRoom ");
            return;
        }

        //isCleared = false;
        UnitSpawner.instance.SpawnEnemy(room,enemySO);
        _barriers.SetActive(true);
    }
    public void SpawnParticularEnemyNoShield(Room room, Enemy_SO enemySO)
    {
        if (room != this || isCleared)
        {// to trigger only on our room
            //Debug.Log("notThisRoom ");
            return;
        }

        //isCleared = false;
        Enemy spawnedEnemy = UnitSpawner.instance.SpawnAndReturnEnemy(room, enemySO);
        spawnedEnemy.DisableShield();
        _barriers.SetActive(true);
        
    }

    public void EnemyDied(Enemy enemy, Room room)
    {
        //Debug.Log("Enemy died in room: "+room.roomID.ToString());
        //Debug.Log("Enemy died in room");
         if (!isCleared)
        {
            //Debug.Log("enemyDied in room not cleared");
            enemiesInRoomCount--;
            if (enemiesInRoomCount <=0)
            {
                //ClearRoom(room);
                //do something
                //Debug.Log("__________________________________________");
                //Debug.Log("Room Clear");
                LiftBarriers(room);//should work without this but here we are
                                   //scheduling it for event does not work
                isCleared = true;
                GameManager.instance.numberRoomsCleared++;
                //DoPostRoom Stuff
                //GlobalEventManager.instance.TriggerOnRoomCleared(this);
                GlobalEventManager.instance.TriggerOnRoomCleared(room);
            }
        }
    }
    public void ClearRoom(Room room)
    {
        //do something
        //Debug.Log("__________________________________________");
        Debug.Log("Room Clear");
        LiftBarriers(room);//should work without this but here we are
                           //scheduling it for event does not work
        isCleared = true;
        GameManager.instance.numberRoomsCleared++;
        //DoPostRoom Stuff
        //GlobalEventManager.instance.TriggerOnRoomCleared(this);
        GlobalEventManager.instance.TriggerOnRoomCleared(room);
    }
    public void ForceRoomCleared(Room room)
    {
        if (room != this)
        {// to trigger only on our room
            //Debug.Log("notThisRoom ");
            return;
        }
        isCleared = true;
        GameManager.instance.numberRoomsCleared++;
        GlobalEventManager.instance.TriggerOnRoomCleared(room);
    }
    public void LiftBarriers(Room room)
    {
        if (room != this)
            {// to trigger only on our room
             //Debug.Log("notThisRoom ");
                return;
            }


        if (_barriers == null)
        {
            Debug.Log("barriers NULL");
        }
        else
        {
            _barriers.SetActive(false);
        }
    }
    
}
