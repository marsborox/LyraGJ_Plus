using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitSpawner : Singleton<UnitSpawner>
{
    public static new UnitSpawner instance => Singleton<UnitSpawner>.instance;
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public List<Enemy_SO> enemy_SOs = new List<Enemy_SO>();
    public GameObject spawnPointsInScene;
    public EnemySpawnChecker enemySpawnCheckerPrefab;
    public EnemySpawnChecker enemySpawnChecker;
    public Enemy enemyPrefab;
    public Player player;

    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 3f;

    bool spawningDone = true;

    public bool spawningAllowed = true;
    [Header("TestSOs")]
    public Enemy_SO meleeSO;
    public Enemy_SO archerSO;
    public Enemy_SO mageSO;

    public List<Enemy_SO>enemySOs = new List<Enemy_SO>();

    [Header("testing randomSpawnPoint position")]
    public Room roomWeTestIn;


    private void Start()
    {
        
        /*EnemySpawnChecker spawnChecker =Instantiate(enemySpawnCheckerPrefab,transform.position,transform.rotation);
        enemySpawnChecker = spawnChecker;*/
    }
    private void Update()
    {
        
    }
    void AddSpawnPointsToList()
    {
        foreach (SpawnPoint spawnPoint in spawnPointsInScene.transform)
        { 
            spawnPoints.Add(spawnPoint);
        }
    }
    public void AutoSpawnEnemies()
    {
        if (!spawningAllowed) return;

        if (spawningDone)
        {
            StartCoroutine(SpawningEnemiesRoutine());

        }
    }
    IEnumerator SpawningEnemiesRoutine()
    {
        spawningDone = false;
        float spawnTime = Random.Range(minSpawnTime,maxSpawnTime);

        yield return new WaitForSeconds(spawnTime);
        SpawnEnemy();
        //StartCoroutine(SpawningEnemiesRoutine());
        spawningDone=true;
    }


    void SpawnEnemy()
    { 
        SpawnPoint spawnPoint;
        int randomIndex = Random.Range(0, spawnPoints.Count);
        spawnPoint = spawnPoints[randomIndex];
        Enemy spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.transform.position,Quaternion.identity);
        Enemy_SO usedTemplate = GetRandomTemplate();
        spawnedEnemy.SetProperties(usedTemplate,player);
        GameManager.instance.enemiesInField++;
    }

    Enemy_SO GetRandomTemplate()
    {
        int randomIndex = Random.Range(0, enemy_SOs.Count);

        return enemy_SOs[randomIndex];
    }
    public void SpawnEnemy(Enemy_SO usedTemplate)
    {
        SpawnPoint spawnPoint;
        int randomIndex = Random.Range(0, spawnPoints.Count);
        spawnPoint = spawnPoints[randomIndex];
        Enemy spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        spawnedEnemy.SetProperties(usedTemplate, player);
        GameManager.instance.enemiesInField++;
    }
    public void SpawnEnemy(Room room, Enemy_SO enemySO)
    {
        Enemy spawnedEnemy = Instantiate(enemyPrefab,room.transform.position,Quaternion.identity);
        spawnedEnemy.SetProperties(enemySO, player,room);
        room.enemiesInRoomCount++;
        GameManager.instance.enemiesInField++;
    }
    public Enemy SpawnAndReturnEnemy(Room room, Enemy_SO enemySO)
    {
        Enemy spawnedEnemy = Instantiate(enemyPrefab, room.transform.position, Quaternion.identity);
        spawnedEnemy.SetProperties(enemySO, player, room);
        room.enemiesInRoomCount++;
        GameManager.instance.enemiesInField++;
        return spawnedEnemy;
    }
    public void SpawnRandomEnemy(float x,float y,Room room)
    {   // this is used !!!!
        //Debug.Log("spawning random test enemy");
        Vector2 spawnPosition = new Vector2(x, y);
        int randomTemplateIndex = Random.Range(0,enemySOs.Count);//may say out of bounds?
        Enemy_SO usedTemplate = enemySOs[randomTemplateIndex];
        Enemy spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemy.SetProperties(usedTemplate,player,room);
        GlobalEventManager.instance.TriggerOnEnemySpawn(spawnedEnemy,room);
        GameManager.instance.enemiesInField++;
        int randomRoll = Random.Range(0,100);
        if (randomRoll > usedTemplate.chanceForShield)
        { 
            spawnedEnemy.DisableShield();
        }
    }

    public void SpawnEnemies(int min, int max)
    {        
        int spawnAmount = UnityEngine.Random.Range(min, max);
        // Debug.Log("How many enemies? " + spawnAmount);
        for (int i = 0; i <= spawnAmount; i++) SpawnEnemy();
    }

    public void TestSpawnMelee()
    {
        SpawnEnemy(meleeSO);
    }
    public void TestSpawnArcher()
    {
        SpawnEnemy(archerSO);
    }
    public void TestSpawnMage()
    {
        SpawnEnemy(mageSO);
    }
    public void TestSpawnPosition()
    {
        Vector2 position;
        
        float x,y;
        roomWeTestIn.ReturnSpawnPoint(out x,out y);
        position = new Vector2(x,y);
        Debug.Log("testing spawnPoint on position X: "+x+"Y: "+y);
        bool isValid = enemySpawnChecker.IsSpawnPosValid(position);
        Debug.Log("spawn point valid is: "+isValid);
    }

}
