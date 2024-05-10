using UnityEngine;
using Zenject;

public class TestPlayerInstaller : MonoInstaller
{
    [SerializeField]
    private Transform playerSpawnPosition;
    [SerializeField]
    private TestPlayer playerPrefab;
    public override void InstallBindings()
    {
        var player = FindAnyObjectByType<TestPlayer>();
        if (!player)
        {
            Debug.LogWarning("No player on scene!");
            return;
        }
        //var player = Container.InstantiatePrefabForComponent<TestPlayer>(playerPrefab, playerSpawnPosition.position, Quaternion.identity, null);
        Container.BindInstance(player);
    }
}