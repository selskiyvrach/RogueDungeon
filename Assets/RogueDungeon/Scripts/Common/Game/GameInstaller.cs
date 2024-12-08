using Common.ZenjectUtils;
using Common.ZenjectUtils.ContextHandles;
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