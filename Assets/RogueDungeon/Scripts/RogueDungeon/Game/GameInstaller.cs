using Common.Game;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/GameInstaller", fileName = "GameInstaller", order = 0)]
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        [SerializeField] private BootstrapStateInstaller _bootstrapStateInstaller;
        [SerializeField] private MainMenuStateInstaller _mainMenuStateInstaller;
        
        public override void InstallBindings()
        {
            Container.Bind<BootstrapStateInstaller>().FromNewScriptableObject(_bootstrapStateInstaller).AsSingle();
            Container.Bind<MainMenuStateInstaller>().FromNewScriptableObject(_mainMenuStateInstaller).AsSingle();
            
            Container.Bind<IGameStatesFactory>().To<GameStateFactory>().FromNew().AsSingle();
            Container.Bind<IGameStateChanger>().To<GameStateChanger>().FromNew().AsSingle();
            Container.Bind<Common.Game.Game>().To<Common.Game.Game>().FromNew().AsSingle().NonLazy();
        }
    }
}