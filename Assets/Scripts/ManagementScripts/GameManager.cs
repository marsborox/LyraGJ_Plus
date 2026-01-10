using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Level_SO;

public enum GameStage {SPAWNING, POSTWAVE, DIALOGUE, NEWWAVE, END}
public class GameManager : Singleton<GameManager>
{
    public static new GameManager instance => Singleton<GameManager>.instance;

    [SerializeField] private GameObject portal;
    [SerializeField] private CutscenesManager cutscenesManager;

    public GameStage stage = GameStage.NEWWAVE;
    public Level_SO levelSettings
    {
        get 
        { 
            if (cutscenesManager != null && cutscenesManager.level != null)
            {
                return cutscenesManager.level; 
            } else {
                return ScriptableObject.CreateInstance<Level_SO>(); // empty but no crashing
            }
        }
    }    
    public int enemiesPerWave = 10;
    public int spawnedEnemiesThisWave = 0;
    public int enemiesInField = 0;

    public int totalEnemyKilled = 0;
    public int roomsCleared = 0;

    public bool isEndOfWave=false;
    public bool isSpawning=false;

    void Start()
    {
        //we wait 1s til leverything really loads
        //StartCoroutine(StartSpawnDelayRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        GameFlow();
        
    }
    void GameFlow()
    {//DISCONTINUED FOR NOW
        switch (stage)
        { 
            case GameStage.SPAWNING:
                {
                    //portal.SetActive(false);
                    //disable portal if is in scene

                    if (spawnedEnemiesThisWave == enemiesPerWave)
                    {
                        stage = GameStage.POSTWAVE;
                    } else
                    {
                        //UnitSpawner.instance.AutoSpawnEnemies();// spawning done elsewhere
                    }
                    break;
                }
            case GameStage.POSTWAVE:
                {
                    isEndOfWave = true;
                    isSpawning = false;
                    spawnedEnemiesThisWave = 0;

                    if (enemiesInField == 0)
                    {
                        // stage = GameStage.DIALOGUE; skipping for now, we need to update dialogues
                        stage = GameStage.END;
                    }
                    
                    break; 
                }
            case GameStage.DIALOGUE:
                {
                    //DisplayDialogue();skipping dialogue for developement
                    //erenable on build
                    stage = GameStage.SPAWNING;
                    break; 
                }
            case GameStage.NEWWAVE: 
                { 
                    break; 
                }
            case GameStage.END:
                {
                    //portal.SetActive(true);
                    //enable portal
                    break;
                }
        }
    }
    public void SpawnDialogue(Room room)
    {
        if (cutscenesManager != null) 
        {
            cutscenesManager.SpawnDialogue(roomsCleared);
        }
    }
    public void ForceRoomCleared(Room room)
    {
        roomsCleared++;
    }
    void ControlGameFlow()
    {
        if (spawnedEnemiesThisWave == enemiesPerWave)
        {
            stage = GameStage.POSTWAVE;
        }
        
    }
    public void PostConversation()
    {
        spawnedEnemiesThisWave = 0;
        isEndOfWave = false;
    }
    public void EnemyDied()
    {
        enemiesInField--;
    }
    public void AcknowledgeSpawnedEnemy()
    {
        spawnedEnemiesThisWave++;
        enemiesInField++;
    }
    IEnumerator StartSpawnDelayRoutine()
    {
        yield return new WaitForSeconds(1f);
        stage = GameStage.SPAWNING;
    }
    public void CountClearedRooms(Room room)
    {
        //Remove This
        //roomsCleared++;
    }
}
