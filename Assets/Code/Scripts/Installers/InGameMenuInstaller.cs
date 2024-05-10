using Code.Scripts.MainMenu;
using Zenject;

namespace Code.Scripts.Installers
{
    public class InGameMenuInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InGameMenuController>().FromNew().AsSingle();
            Container.Bind<SettingsController>().FromNew().AsSingle();
        }
    }
}