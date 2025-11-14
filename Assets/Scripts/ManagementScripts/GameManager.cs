using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.UI;

public enum GameStage {SPAWNING, POSTWAVE, DIALOGUE, NEWWAVE, END}
public class GameManager : Singleton<GameManager>
{
    public static new GameManager instance => Singleton<GameManager>.instance;

    public GameStage stage = GameStage.NEWWAVE;

    public int enemiesPerWave = 10;

    public GameObject portal;
    public int spawnedEnemiesThisWave = 0;
    public int enemiesInField = 0;

    public bool isEndOfWave=false;
    public bool isSpawning=false;

    public DialogueUI dialogueUI;
    public List<Dialogue_SO> dialogueSOs = new List<Dialogue_SO>();

    private int _dialogueStage = 0;
    private int _dialogPart = 0;
    void Start()
    {
        //we wait 1s til leverything really loads
        StartCoroutine(StartSpawnDelayRoutine());
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
                    portal.SetActive(false);

                    if (spawnedEnemiesThisWave == enemiesPerWave)
                    {
                        stage = GameStage.POSTWAVE;
                    } else
                    {
                        UnitSpawner.instance.AutoSpawnEnemies();
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
                    portal.SetActive(true);
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
        _dialogPart++;

        Dialogue_SO dialogue = dialogueSOs[_dialogueStage];
        if (_dialogPart < dialogue.parts.Length)
        {
            Debug.Log("Let's continue dialog!");
            PrepareDialog();
        }
        else
        {
            Debug.Log("NO more talking!");
            dialogueUI.gameObject.SetActive(false);

            stage = GameStage.SPAWNING;
            _dialogueStage++;
            Time.timeScale = 1f;
            if (_dialogueStage > (dialogueSOs.Count - 1))//bcs count is max index+1
            {
                _dialogueStage = 0;
            }
            _dialogPart = 0;
        }
    }
    private void PrepareDialog()
    {
        Dialogue_SO dialogue = dialogueSOs[_dialogueStage];
        if (_dialogPart < dialogue.parts.Length)
        {
            DialoguePart part = dialogue.parts[_dialogPart];
            dialogueUI.textOfDialogue.text = part.dialogueText;
            dialogueUI.characterImage.sprite = part.characterImage;
            Debug.Log(_dialogPart);
        }
    }
}
