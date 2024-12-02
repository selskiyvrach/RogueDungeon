using Common.Game;
using Common.ZenjectUtils;
using Common.ZenjectUtils.ContextHandles;
using Zenject;

namespace RogueDungeon.Game
{
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.NewSingle<GameContextHandle>();
            Container.NewSingle<IGameStateChanger, GameStateChanger>();
            Container.NewSingle<Common.Game.Game>();
        }
    }
}