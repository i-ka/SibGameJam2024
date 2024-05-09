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
        var player = Container.InstantiatePrefabForComponent<TestPlayer>(playerPrefab);
        Container.BindInstance(player);
    }
}