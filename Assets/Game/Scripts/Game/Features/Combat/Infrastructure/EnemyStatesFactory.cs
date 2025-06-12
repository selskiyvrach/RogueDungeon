using Game.Features.Combat.Domain.Enemies;
using Libs.Movesets;
using Zenject;

namespace Game.Features.Combat.Infrastructure
{
    public class EnemyStatesFactory : IFactory<string, EnemyMove>
    {
        private readonly MoveSetFactory _moveSetFactory;
        private readonly EnemyConfig _config;

        public EnemyStatesFactory(EnemyConfig config, MoveSetFactory moveSetFactory)
        {
            _config = config;
            _moveSetFactory = moveSetFactory;
        }

        public EnemyMove Create(string name) =>
            (EnemyMove)_moveSetFactory.CreateMove(_config.GetMoveArgs(name));
    }
}