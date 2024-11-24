using RogueDungeon.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/MainMenuInstaller", fileName = "MainMenuInstaller", order = 0)]
    public class MainMenuInstaller : ScriptableObjectInstaller<MainMenuInstaller>
    {
        [SerializeField] private MainMenuView _mainMenuView;
        
        public override void InstallBindings()
        {
            Container.Bind<IMainMenuView>().To<MainMenuView>().FromInstance(_mainMenuView);
            
        }
    }
    
}