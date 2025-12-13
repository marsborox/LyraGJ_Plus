using UnityEngine;

public class GameUI : MonoBehaviour/*SingletonPersistent<GameUI>*/
{
    //public static new UnitSpawner instance => Singleton<UnitSpawner>.instance;
    public PlayerCombat playerCombat;

    void Start()
    { 
        playerCombat = (PlayerCombat)PlayerSingleton.instance.player.unitCombat;
    }
}
