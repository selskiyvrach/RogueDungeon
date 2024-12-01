using Common.Game;
using Common.InstallerGenerator;
using Zenject;

namespace RogueDungeon.Game
{
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.NewSingle<IGameStatesFactory, GameStateFactory>();
            Container.NewSingle<IGameStateChanger, GameStateChanger>();
            Container.Bind<Common.Game.Game>().To<Common.Game.Game>().FromNew().AsSingle().NonLazy();
        }
    }
}