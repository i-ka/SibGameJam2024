using Code.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenuView mainMenu;
    [SerializeField] private SettingsMenuView settingsMenu;
    
    public override void InstallBindings()
    {
        Container.Bind<MainMenuController>().FromNew().AsSingle();
        Container.Bind<SettingsController>().FromNew().AsSingle();
        
        var settingsMenuInstance = Container.InstantiatePrefabForComponent<SettingsMenuView>(settingsMenu);
        Container.Bind<SettingsMenuView>().FromInstance(settingsMenuInstance).AsSingle();
        settingsMenuInstance.gameObject.SetActive(false);
        
        var mainMenuInstance = Container.InstantiatePrefabForComponent<MainMenuView>(mainMenu);
        Container.Bind<MainMenuView>().FromInstance(mainMenuInstance).AsSingle();
        

    }
}