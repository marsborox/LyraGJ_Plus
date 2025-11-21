using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

using UnityEngine;

public class UnitSpawner : Singleton<UnitSpawner>
{
    public static new UnitSpawner instance => Singleton<UnitSpawner>.instance;
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public List<Enemy_SO> enemy_SOs = new List<Enemy_SO>();
    public GameObject spawnPointsInScene;

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
    private void Start()
    {

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
        GameManager.instance.AcknowledgeSpawnedEnemy();
        spawnedEnemy.SetProperties(usedTemplate,player);
    }

    Enemy_SO GetRandomTemplate()
    {
        int randomIndex = Random.Range(0, enemy_SOs.Count);

        return enemy_SOs[randomIndex];
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
    void SpawnEnemy
        (Enemy_SO usedTemplate)
    {
        SpawnPoint spawnPoint;
        int randomIndex = Random.Range(0, spawnPoints.Count);
        spawnPoint = spawnPoints[randomIndex];
        Enemy spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        
        spawnedEnemy.SetProperties(usedTemplate, player);
    }
}
