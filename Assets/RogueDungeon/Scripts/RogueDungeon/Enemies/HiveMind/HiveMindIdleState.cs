using System.Linq;
using Common.Fsm;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindIdleState : HiveMindState
    {
        private readonly HiveMindConfig _config;
        private readonly HiveMindContext _context;
        private readonly IEnemiesRegistry _enemiesRegistry;

        protected override bool IsSlackFrame => true;

        public HiveMindIdleState(IEnemiesRegistry enemiesRegistry, HiveMindContext context, HiveMindConfig config) : base(context)
        {
            _enemiesRegistry = enemiesRegistry;
            _context = context;
            _config = config;
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            var enemies = _enemiesRegistry.Enemies;
            if (enemies.Any() && enemies.All(n => n.CombatPosition is not EnemyPosition.Middle and not EnemyPosition.ChangingPosition))
            {
                _context.EnemiesToMove.Add((enemies.First(), EnemyPosition.Middle));
                stateChanger.ChangeState<HiveMindMoveEnemiesState>();
            }

            if (_context.SlackTime >= _config.SlackTime && enemies.Any(n => n.IsIdle))
                stateChanger.ChangeState<HiveMindAttackState>();
        }
    }
}