using Code.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenuView mainMenu;

    [SerializeField] private AudioListener audioListener;
    
    public override void InstallBindings()
    {
        Container.Bind<MainMenuController>().FromNew().AsSingle();
        Container.Bind<SettingsController>().FromNew().AsSingle();
        
        var mainMenuInstance = Container.InstantiatePrefabForComponent<MainMenuView>(mainMenu);
        Container.Bind<MainMenuView>().FromInstance(mainMenuInstance).AsSingle();
        
    }
}