using Common.MoveSets;
using RogueDungeon.Enemies.States;
using Zenject;

namespace RogueDungeon.Enemies
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