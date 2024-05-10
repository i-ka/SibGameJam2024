using Code.Scripts;
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
        Container.Bind<GameFlowController>().FromNew().AsSingle();
        var player = FindAnyObjectByType<TestPlayer>();
        if (!player)
        {
            Debug.LogWarning("No player on scene!");
            return;
        }
        Container.BindInstance(player);
    }
}