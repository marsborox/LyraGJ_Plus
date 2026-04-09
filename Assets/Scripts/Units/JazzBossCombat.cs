using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JazzBossCombat : MonoBehaviour
{
    public enum State { WAITING, SPAWN_ENEMIES, SPAWN_NOTE, BAD_JAZZ, FINAL_SONG}

    [Header("Boss")]
    [SerializeField] private Animator animator;
    [SerializeField] private Image jazzMeter;
    [SerializeField] private float jazz = 0f;
    [SerializeField] private float maxJazz = 100f;
    [SerializeField] private float decreaseJazzInterval = 10f;
    [SerializeField] private SpotlightChangingColors spotlight;

    [Header("Enemies")]
    [SerializeField] private UnitSpawner unitSpawner;
    [SerializeField] private int minSpawnOfEnemies;
    [SerializeField] private int maxSpawnOfEnemies;

    [Header("Note")]
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Vector2 noteSpawnPosition;

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
    private Coroutine _enemiesCheckRoutine;

    void Start()
    {
        Debug.Log("Start Enemies: " + GameManager.instance.enemiesInField);
        GlobalEventManager.OnEnemyDied += OnEnemyDied;

        RefreshJazzMeter();
        StartCoroutine(DecreaseJazzMeter());

        currentState = State.SPAWN_ENEMIES;
    }

    void OnDestroy()
    {
        GlobalEventManager.OnEnemyDied -= OnEnemyDied;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == _note)
        {
            OnCollideWithJazzNote();
        }
    }

    private void OnChangeState()
    {
        if (currentState != State.SPAWN_ENEMIES)
        {
            StopCoroutine(_enemiesCheckRoutine);
        }

        switch (currentState) {
            case State.WAITING:
                break;
            case State.SPAWN_ENEMIES: 
            {
                unitSpawner.SpawnEnemies(minSpawnOfEnemies, maxSpawnOfEnemies);
                _enemiesCheckRoutine = StartCoroutine(CheckEnemiesCount());
                break;
            }
            case State.SPAWN_NOTE:
            {
                _note = Instantiate(notePrefab, noteSpawnPosition, Quaternion.identity).GetComponent<JazzNote>();
                _note.spotlight = spotlight;
                _note.target = transform;
                break;
            }
            case State.BAD_JAZZ:
                MySoundManager.instance.PlayBadJazzBossMusic();
                break;
            case State.FINAL_SONG:
                MySoundManager.instance.PlayFinalJazzBossSong();
                break;
        }
    }

    private void OnEnemyDied(Enemy enemy,Room room)
    {
        noteSpawnPosition = enemy.gameObject.transform.position;

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

        if (_note.jazzBoost == 0)
        {
            currentState = State.SPAWN_ENEMIES;
        } else
        {
            if (jazz < maxJazz)
            {
                currentState = State.BAD_JAZZ;
            } else
            {
                currentState = State.FINAL_SONG;
            }
        }

        Destroy(_note);
    }

    private void RefreshJazzMeter()
    {
        float jazzLevel = jazz / maxJazz;
        jazzMeter.fillAmount = jazzLevel;
    }

    private IEnumerator DecreaseJazzMeter()
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseJazzInterval);

            if (jazz > 0)
            {
                jazz -= 1f;
                RefreshJazzMeter();
            }
        }
    }

    // TEMPORARY CODE

    IEnumerator CheckEnemiesCount()
    {
        while (true) // Loop indefinitely
        {
            if (currentState == State.SPAWN_ENEMIES)
            {
                if (GameManager.instance.enemiesInField == 0)
                {
                    Debug.Log("No more ENEMIES");
                    currentState = State.SPAWN_NOTE;                                        
                }
                else
                {
                    Debug.Log("Enemies: " + GameManager.instance.enemiesInField);
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
