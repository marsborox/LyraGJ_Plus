using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public static new PlayerManager instance => Singleton<PlayerManager>.instance;
    [SerializeField] private Player _player;

    public Player returnPlayer()
    {
        return _player;
    }
}
