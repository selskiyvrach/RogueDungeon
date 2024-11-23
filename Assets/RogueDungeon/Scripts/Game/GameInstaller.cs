using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/GameInstaller", fileName = "GameInstaller", order = 0)]
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        [SerializeField] private BootstrapStateInstaller _bootstrapStateInstaller;
        
        public override void InstallBindings()
        {
            Container.Bind<BootstrapStateInstaller>().FromNewScriptableObject(_bootstrapStateInstaller).AsSingle();
            
            Container.Bind<IGameStatesFactory>().To<GameStateFactory>().FromNew().AsSingle();
            Container.Bind<IGameStateChanger>().To<GameStateChanger>().FromNew().AsSingle();
            Container.Bind<Game>().To<Game>().FromNew().AsSingle().NonLazy();
        }
    }
}