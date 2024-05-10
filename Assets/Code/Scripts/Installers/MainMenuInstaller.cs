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
    }
}