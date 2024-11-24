using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/MainMenuInstaller", fileName = "MainMenuInstaller", order = 0)]
    public class MainMenuStateInstaller : ScriptableObjectInstaller<MainMenuStateInstaller>
    {
        [SerializeField] private MainMenuMvvmFactory _mainMenuMvvmFactory;
        
        public IMainMenuModel Resolve()
        {
            var subContainer = Container.CreateSubContainer();
            subContainer.Bind<IFactory<IMainMenuModel>>().To<MainMenuMvvmFactory>().FromNewScriptableObject(_mainMenuMvvmFactory).AsSingle();
            subContainer.Bind<IMainMenuModel>().FromMethod(_ => subContainer.Resolve<IFactory<IMainMenuModel>>().Create()).AsSingle();
            return subContainer.Resolve<IMainMenuModel>();
        }
    }
}