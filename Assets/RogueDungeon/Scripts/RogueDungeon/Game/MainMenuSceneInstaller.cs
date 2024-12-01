using Common.InstallerGenerator;
using RogueDungeon.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _view;
        
        public override void InstallBindings()
        {
            Container.NewSingle<IMainMenuViewModel, MainMenuViewModel>();
            Container.NewSingle<IMainMenuModel, MainMenuModel>();
            Container.Bind<MainMenuView>().FromInstance(_view);
        }
    }
}