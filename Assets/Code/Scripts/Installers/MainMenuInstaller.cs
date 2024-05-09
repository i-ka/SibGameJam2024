using Code.Scripts.MainMenu;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenuView mainMenu;
    
    public override void InstallBindings()
    {
        Container.Bind<MainMenuController>().FromNew().AsSingle();
        
        var mainMenuInstance = Container.InstantiatePrefabForComponent<MainMenuView>(mainMenu);
        Container.Bind<MainMenuView>().FromInstance(mainMenuInstance).AsSingle();
        
    }
}