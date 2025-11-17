using UnityEngine;
using UnityEngine.UI;

public class TestUI : UI
{
    [SerializeField] private Button _spawnMeleeButton;
    [SerializeField] private Button _spawnArcherButton;
    [SerializeField] private Button _spawnMageButton;
    private void Start()
    {
        InitiateButton(_spawnMeleeButton,UnitSpawner.instance.TestSpawnMelee);
        InitiateButton(_spawnArcherButton, UnitSpawner.instance.TestSpawnArcher);
        InitiateButton(_spawnMageButton,UnitSpawner.instance.TestSpawnMage);
    }
}
