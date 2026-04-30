using UnityEngine;

public class GameUI : Singleton<GameUI>
{
    public static new GameUI instance => Singleton<GameUI>.instance;
    public PlayerDied_UI playerDied_UI;

    [SerializeField] private WeaponsSetup_UI _weaponSetup_UI;

    private void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        GlobalEventManager.OnPlayerDied += OpenPlayerDiedUI;
    }
    private void OnDisable()
    {
        GlobalEventManager.OnPlayerDied -= OpenPlayerDiedUI;
    }
    private void OpenPlayerDiedUI()
    { 
        playerDied_UI.gameObject.SetActive(true);
    }
    public void OpenCloseWeaponsUI()
    {
        if (_weaponSetup_UI.gameObject.activeSelf)
        {
            _weaponSetup_UI.gameObject.SetActive(false);
        }
        else if (!_weaponSetup_UI.gameObject.activeSelf)
        { 
            _weaponSetup_UI.gameObject.SetActive(true);
        }
    }

}
