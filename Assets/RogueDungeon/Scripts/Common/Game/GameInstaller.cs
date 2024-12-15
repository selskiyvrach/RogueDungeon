using Common.UtilsZenject;
using Common.UtilsZenject.ContextHandles;
using Zenject;

namespace Common.Game
{
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.NewSingle<GameContextHandle>();
            Container.NewSingle<IGameStateChanger, GameStateChanger>();
            Container.NewSingle<Game>();
        }
    }
}