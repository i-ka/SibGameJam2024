using Code.Scripts;
using UnityEngine;
using Zenject;

public class TestPlayerInstaller : MonoInstaller
{
    [SerializeField] private GameGoalSettings goalSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(goalSettings).AsSingle();
        Container.Bind<GameStateController>().FromNew().AsSingle();
        
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