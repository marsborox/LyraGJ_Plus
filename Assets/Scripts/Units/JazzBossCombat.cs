using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JazzBossCombat : MonoBehaviour
{
    public enum State { INTRO, WAITING, SPAWN_ENEMIES, SPAWN_NOTE, BAD_JAZZ, OUTRO}

    [Header("Boss")]
    [SerializeField] private Animator animator;
    [SerializeField] private float jazz = 0f;
    [SerializeField] private float maxJazz = 10f;
    [SerializeField] private float dropJazzInterval = 5f;
    [SerializeField] private float dropJazzValue = 0.3f;
    [SerializeField] private SpotlightChangingColors spotlight;

    [Header("Jazz")]
    [SerializeField] private GameObject jazzMeter;
    [SerializeField] private Image jazzValue;

    [Header("Enemies")]
    [SerializeField] private UnitSpawner unitSpawner;
    [SerializeField] private int minSpawnOfEnemies = 2;
    [SerializeField] private int maxSpawnOfEnemies = 7;

    [Header("Note")]
    [SerializeField] private GameObject notePrefab;

    [Header("Cutscene Dialogues")]
    [SerializeField] private CutscenesPlayer cutscenesPlayer;
    [SerializeField] private Dialogue_SO intro;
    [SerializeField] private Dialogue_SO afterHittingBoss;
    [SerializeField] private Dialogue_SO afterMissingNote1;
    [SerializeField] private Dialogue_SO afterMissingNote2;
    [SerializeField] private Dialogue_SO outro;

    public State currentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) return; // no change, stop

            _currentState = value;
            OnChangeState();
        }
    }

    private State _currentState;
    private JazzNote _note;
    private Vector2 _noteSpawnPosition;
    private Coroutine _dropJazzRoutine;

    void Start()
    {
        GlobalEventManager.OnEnemyDied += OnEnemyDied;

        RefreshJazzMeter();
        _dropJazzRoutine = StartCoroutine(DropJazzMeter());

        currentState = State.INTRO;
        OnChangeState();
    }

    void OnDestroy()
    {
        GlobalEventManager.OnEnemyDied -= OnEnemyDied;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision with " + collision.gameObject.tag);
        if (collision.gameObject.tag == "JazzNote")
        {
            OnCollideWithJazzNote();
        }
        else if (collision.gameObject.tag == "PlayerWeapon" || collision.gameObject.tag == "PlayerProjectile")
        {
            // TODO: move boss to podium, disable collisions with him from here
            cutscenesPlayer.SpawnDialogue(afterHittingBoss, () => currentState = State.SPAWN_ENEMIES);
        }
    }

    private void OnChangeState()
    {
        Debug.Log("Changing state");
        animator.Play("BossJazzIdle");

        switch (currentState) {
            case State.INTRO:
                cutscenesPlayer.SpawnDialogue(intro, () => currentState = State.WAITING);
                break;
            case State.WAITING:
                break;
            case State.SPAWN_ENEMIES: 
            {
                unitSpawner.SpawnEnemies(minSpawnOfEnemies, maxSpawnOfEnemies);
                break;
            }
            case State.SPAWN_NOTE:
            {
                _note = Instantiate(notePrefab, _noteSpawnPosition, Quaternion.identity).GetComponent<JazzNote>();
                _note.spotlight = spotlight;
                _note.target = transform;
                break;
            }
            case State.BAD_JAZZ:
                {
                    animator.Play("BossJazzDancing");
                    StartCoroutine(PlayBaddJazz(3));
                    break;            
                }
            case State.OUTRO:
            {
                animator.Play("BossJazzPlaying");

                StopCoroutine(_dropJazzRoutine);
                jazzMeter.SetActive(false);

                spotlight.isChangingColors = false;
                
                MySoundManager.instance.PlayFinalJazzBossSong();
                break;
            }
        }
    }

    private void OnEnemyDied(Enemy enemy,Room room)
    {
        _noteSpawnPosition = enemy.gameObject.transform.position;

        if (currentState != State.SPAWN_ENEMIES)
        {
            Debug.LogError("Enemy is not supposed to be dying now.");
            return;
        }

        if (GameManager.instance.enemiesInField == 0)
        {
            currentState = State.SPAWN_NOTE;
        }
    }

    private void OnCollideWithJazzNote()
    {
        jazz += _note.jazzBoost;
        RefreshJazzMeter();

        if (jazz < maxJazz)
        {
            currentState = State.BAD_JAZZ;
        } else
        {
            currentState = State.OUTRO;
        }

        Destroy(_note.gameObject);
    }

    private void RefreshJazzMeter()
    {
        float jazzLevel = jazz / maxJazz;
        jazzValue.fillAmount = jazzLevel;
    }

    private IEnumerator DropJazzMeter()
    {
        while (true)
        {
            yield return new WaitForSeconds(dropJazzInterval);

            if (jazz > 0)
            {
                jazz -= dropJazzValue;
                RefreshJazzMeter();
            }
        }
    }

    private IEnumerator PlayBaddJazz(int howManyTimes)
    {
        int counter = 0;
        float interval = 0.7f;
        while (counter < howManyTimes)
        {
            MySoundManager.instance.PlayBadJazzBossMusic();
            yield return new WaitForSeconds(interval);
            counter++;
        }

        currentState = State.SPAWN_ENEMIES;
    }
}
