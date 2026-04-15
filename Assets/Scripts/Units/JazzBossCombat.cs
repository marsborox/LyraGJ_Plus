using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JazzBossCombat : MonoBehaviour
{
    public enum State { INTRO, WAITING, SPAWN_ENEMIES, SPAWN_NOTE, BAD_JAZZ, OUTRO}

    [Header("Boss")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private float jazz = 0f;
    [SerializeField] private float maxJazz = 10f;
    [SerializeField] private float dropJazzInterval = 5f;
    [SerializeField] private float dropJazzValue = 0.3f;
    [SerializeField] private Vector2 podiumPosition;
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
    private int _missedNote;

    void Start()
    {
        GlobalEventManager.OnEnemyDied += OnEnemyDied;

        RefreshJazzMeter();
        _dropJazzRoutine = StartCoroutine(DropJazzMeter());

        currentState = State.INTRO;
        OnChangeState(); // force update
    }

    void OnDestroy()
    {
        GlobalEventManager.OnEnemyDied -= OnEnemyDied;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "JazzNote")
        {
            OnCollideWithJazzNote();
        }
        else if (collision.gameObject.tag == "PlayerWeapon" || collision.gameObject.tag == "PlayerProjectile")
        {
            StartCoroutine(FlyBossToPosition(podiumPosition, 0.09f));
        }
    }

    private void OnChangeState()
    {
        animator.Play("BossJazzIdle");

        switch (currentState) {
            case State.INTRO: 
            {
                StartCoroutine(PlayBadJazz(6));
                // MySoundManager.instance.PlayBadJazzBossMusic();

                _missedNote = 0;
                cutscenesPlayer.SpawnDialogue(intro, () => currentState = State.WAITING);
                break;
            }
            case State.WAITING:
                StartCoroutine(PlayBadJazz(3));
                // MySoundManager.instance.PlayBadJazzBossMusic();
                break;
            case State.SPAWN_ENEMIES: 
            {
                unitSpawner.SpawnEnemies(minSpawnOfEnemies, maxSpawnOfEnemies);
                GlobalEventManager.instance.TriggerOnPlayerAtack();
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
                    StartCoroutine(PlayBadJazz(3));
                    break;            
                }
            case State.OUTRO:
            {
                StopCoroutine(_dropJazzRoutine);

                animator.Play("BossJazzPlaying");
                StartCoroutine(PlayGoodJazz(3f));
                break;
            }
        }
    }

    // Events

    private void OnEnemyDied(Enemy enemy,Room room)
    {
        StartCoroutine(PlayBadJazz(2));
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

        if (_note.jazzBoost == 0)
        {
            _missedNote++;
            if (_missedNote == 1)
            {
                cutscenesPlayer.SpawnDialogue(afterMissingNote1, null);
            }
            else if (_missedNote == 2)
            {
                cutscenesPlayer.SpawnDialogue(afterMissingNote2, null);                
            }
        }

        Destroy(_note.gameObject);
    }

    // UI
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

    // Helpers

    private IEnumerator FlyBossToPosition(Vector2 position, float duration)
    {
        spriteRenderer.sortingOrder = 1; // podium utilizes same layer as units for player to nicely wrap around it

        float elapsed = 0;
        Vector3 startPos = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector2.Lerp(startPos, position, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.position = position;

        yield return new WaitForSeconds(duration + 1f); // add some buffer for player to see that hitting a boss did NOT work
        cutscenesPlayer.SpawnDialogue(afterHittingBoss, () => currentState = State.SPAWN_ENEMIES);
    }
    private IEnumerator PlayBadJazz(int howManyTimes)
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
    private IEnumerator PlayGoodJazz(float seconds)
    {
        jazzMeter.SetActive(false);
        spotlight.isChangingColors = false;

        MySoundManager.instance.PlayFinalJazzBossSong();

        yield return new WaitForSeconds(seconds);

        cutscenesPlayer.SpawnDialogue(outro, () => MySceneManager.instance.OpenLobby());
    }
}
