using System.Linq;
using Common.Fsm;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindIdleState : HiveMindState
    {
        private readonly HiveMindConfig _config;
        private readonly HiveMind _context;

        public HiveMindIdleState(HiveMind context, HiveMindConfig config)
        {
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
            var enemies = _context.Enemies;
            if (enemies.Any() && enemies.All(n => n.OccupiedPosition is not EnemyPosition.Middle))
            {
                _context.EnemiesToMove.Add((enemies.First(), EnemyPosition.Middle));
                stateChanger.ChangeState<HiveMindMoveEnemiesState>();
            }

            if (_context.SlackTime >= _config.SlackTime && enemies.Any(n => n.CanAttack))
                stateChanger.ChangeState<HiveMindAttackState>();
        }
    }
}