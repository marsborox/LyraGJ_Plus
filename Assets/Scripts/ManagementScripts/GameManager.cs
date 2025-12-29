using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

using static Level_SO;

public enum GameStage {SPAWNING, POSTWAVE, DIALOGUE, NEWWAVE, END}
public class GameManager : Singleton<GameManager>
{
    public static new GameManager instance => Singleton<GameManager>.instance;

    public GameStage stage = GameStage.NEWWAVE;

    public GameObject portal;
    public DialogueUI dialogueUI;

    public Level_SO levelSettings;

    
    public int enemiesPerWave = 10;
    public int spawnedEnemiesThisWave = 0;
    public int enemiesInField = 0;

    public int totalEnemyKilled = 0;
    public int roomsCleared = 0;

    public bool isEndOfWave=false;
    public bool isSpawning=false;

    public List<Dialogue_SO> dialogueSOs = new List<Dialogue_SO>();

    private int _dialogueStage = 0;
    private int _dialogPart = 0;
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
    {
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
                    //portal.SetActive(true);
                    //enable portal
                    break;
                }
        }
    }
    public void PostRoomCleared(Room room)
    {
        roomsCleared++;
        SpawnDialogue(roomsCleared);
        //if(roomsCleared = levelSettings.)
    }
    public void SpawnDialogue(int indexOfClearedRoom)
    {
        int returnIndex = int.MaxValue;
        DialogueToIndex dialogueToIndexReturn;

        foreach (DialogueToIndex dialogueToIndex in levelSettings.dialogueWRoomClearedIndexList)
        {
            if (dialogueToIndex.spawnOnRoomCleared < returnIndex)
            {
                returnIndex = dialogueToIndex.spawnOnRoomCleared;
                dialogueToIndexReturn = dialogueToIndex;

                if (returnIndex == indexOfClearedRoom)
                {
                    ProcessDialogue(dialogueToIndexReturn.dialogue);
                }
            }
        }
    }
    #region NewDialogueLogic


    private void ProcessDialogue(Dialogue_SO dialogue)
    {
        Time.timeScale = 0f;//pause

        dialogueUI.gameObject.SetActive(true);
        dialogueUI.characterImage.SetNativeSize();

        PrepareDialogue(dialogue);
    }
    private void PrepareDialogue(Dialogue_SO dialogue)
    {
        dialogue = dialogueSOs[_dialogueStage];
        if (_dialogPart < dialogue.parts.Length)
        {
            DialoguePart part = dialogue.parts[_dialogPart];
            dialogueUI.textOfDialogue.text = part.dialogueText;
            dialogueUI.characterImage.sprite = part.characterImage;
            Debug.Log(_dialogPart);
        }
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

    #endregion
    #region originalDialogueLogic
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
    #endregion


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
        roomsCleared++;
    }
}
