using UnityEngine;

public class PlayerSingleton : SingletonPersistent<PlayerSingleton>
{
    public static new PlayerSingleton instance => SingletonPersistent<PlayerSingleton>.instance;
    public Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
