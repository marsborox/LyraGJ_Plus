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
        spawnedEnemy.player = player;
        Enemy_SO usedTemplate = GetRandomTemplate();
        GameManager.instance.AcknowledgeSpawnedEnemy();
        SetEnemyProperties(spawnedEnemy, GetRandomTemplate());
    }
    void SetEnemyProperties(Enemy enemy, Enemy_SO enemyTemplate)
    { 
        enemy.range = enemyTemplate.range;
        enemy.damage = enemyTemplate.damage;
        enemy.attackCooldown = enemyTemplate.attackCooldown;
        enemy.movementSpeed = enemyTemplate.movementSpeed;

        enemy.SetEnemyType(enemyTemplate.enemyType);
        enemy.health = enemyTemplate.health;

        //enemy.goingUp.GetComponent<SpriteRenderer>().color = enemyTemplate.spriteColor;
        //enemy.goingDown.GetComponent<SpriteRenderer>().color = enemyTemplate.spriteColor;
        //enemy.goingLeft.GetComponent<SpriteRenderer>().color = enemyTemplate.spriteColor;

    }
    Enemy_SO GetRandomTemplate()
    {
        int randomIndex = Random.Range(0, enemy_SOs.Count);

        return enemy_SOs[randomIndex];
    }
}
