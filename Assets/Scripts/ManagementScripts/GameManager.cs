using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.UI;

public enum GameStage {SPAWNING, POSTWAVE,DIALOGUE,NEWWAVE }
public class GameManager : Singleton<GameManager>
{
    public static new GameManager isntance => Singleton<GameManager>.instance;

    public GameStage stage = GameStage.NEWWAVE;

    public int enemiesPerWave = 10;
    int realEnemiesPerWave;//no time to fix why it spawns 1 extra 

    public int spawnedEnemiesThisWave = 0;
    public int enemiesInField = 0;


    public bool isEndOfWave=false;
    public bool isSpawning=false;

    int dialogueStage = 0;
    int dialogPart = 0;
    public DialogueUI dialogueUI;
    public List<Dialogue_SO> dialogueSOs = new List<Dialogue_SO>();
    void Start()
    {
        //we wait 1s til leverything really loads
        StartCoroutine(StartSpawnDelayRoutine());
        realEnemiesPerWave = enemiesPerWave - 1;
    }

    // Update is called once per frame
    void Update()
    {
        GameFlow();
        
    }
    void GameFlow()
    {
        switch (stage)
        { 
            case GameStage.SPAWNING:
                {
                    UnitSpawner.instance.AutoSpawnEnemies();
                    if (spawnedEnemiesThisWave == enemiesPerWave)
                    {
                        stage = GameStage.POSTWAVE;
                    }
                    break;
                }
            case GameStage.POSTWAVE:
                {
                    isEndOfWave = true;
                    isSpawning = false;
                    spawnedEnemiesThisWave = 0;
                    //display conversation
                    if (enemiesInField == 0)
                    {
                        stage = GameStage.DIALOGUE;
                    }
                    
                    break; 
                }
            case GameStage.DIALOGUE:
                {
                    DisplayDialogue();
                    break; 
                }
            case GameStage.NEWWAVE: 
                { 
                    break; 
                }
        }
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
    public void DisplayDialogue()
    {
        Time.timeScale = 0f;//pause

        dialogueUI.gameObject.SetActive(true);
        dialogueUI.characterImage.SetNativeSize();

        PrepareDialog();
    }
    public void ContinueDialogue()
    {
        dialogPart++;

        Dialogue_SO dialogue = dialogueSOs[dialogueStage];
        if (dialogPart < dialogue.parts.Length)
        {
            Debug.Log("Let's continue dialog!");
            PrepareDialog();
        }
        else
        {
            Debug.Log("NO more talking!");
            dialogueUI.gameObject.SetActive(false);

            stage = GameStage.SPAWNING;
            dialogueStage++;
            Time.timeScale = 1f;
            if (dialogueStage > (dialogueSOs.Count - 1))//bcs count is max index+1
            {
                dialogueStage = 0;
            }
            dialogPart = 0;
        }
    }
    private void PrepareDialog()
    {
        Dialogue_SO dialogue = dialogueSOs[dialogueStage];
        if (dialogPart < dialogue.parts.Length)
        {
            DialoguePart part = dialogue.parts[dialogPart];
            dialogueUI.textOfDialogue.text = part.dialogueText;
            dialogueUI.characterImage.sprite = part.characterImage;
            Debug.Log(dialogPart);
        }
    }
}
