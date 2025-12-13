using UnityEngine;

public class PlayerSpawnPoint : Singleton<PlayerSpawnPoint>
{
    public static new PlayerSpawnPoint instance => Singleton<PlayerSpawnPoint>.instance;
}
