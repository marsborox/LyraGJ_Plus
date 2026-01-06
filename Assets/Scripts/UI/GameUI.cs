using UnityEngine;

public class GameUI : MonoBehaviour/*SingletonPersistent<GameUI>*/
{
    //public static new UnitSpawner instance => Singleton<UnitSpawner>.instance;
    public PlayerCombat playerCombat;
    public PlayerDied_UI playerDied_UI;

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
}
