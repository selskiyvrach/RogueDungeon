using System.Linq;
using Common.Fsm;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindIdleState : HiveMindState
    {
        private readonly HiveMindConfig _config;
        private readonly HiveMind _context;
        private readonly IEnemiesRegistry _enemiesRegistry;

        public HiveMindIdleState(IEnemiesRegistry enemiesRegistry, HiveMind context, HiveMindConfig config)
        {
            _enemiesRegistry = enemiesRegistry;
            _context = context;
            _config = config;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            _context.SlackTime += timeDelta;
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            var enemies = _enemiesRegistry.Enemies;
            if (enemies.Any() && enemies.All(n => n.TargetablePosition is not EnemyPosition.Middle))
            {
                _context.EnemiesToMove.Add((enemies.First(), EnemyPosition.Middle));
                stateChanger.ChangeState<HiveMindMoveEnemiesState>();
            }

            if (_context.SlackTime >= _config.SlackTime && enemies.Any(n => n.IsIdle))
                stateChanger.ChangeState<HiveMindAttackState>();
        }
    }
}