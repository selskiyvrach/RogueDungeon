using System.Linq;
using Common.Fsm;
using RogueDungeon.Combat;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindIdleState : HiveMindState
    {
        private readonly HiveMindConfig _config;
        private readonly HiveMindContext _context;
        private readonly ICombatantsRegistry _combatantsRegistry;

        protected override bool IsSlackFrame => true;

        public HiveMindIdleState(ICombatantsRegistry combatantsRegistry, HiveMindContext context, HiveMindConfig config) : base(context)
        {
            _combatantsRegistry = combatantsRegistry;
            _context = context;
            _config = config;
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            var enemies = _combatantsRegistry.Enemies;
            if (enemies.Any() && enemies.All(n => n.CombatPosition != EnemyPosition.Middle))
            {
                _context.EnemiesToMove.Add(((Enemy)enemies.First(), EnemyPosition.Middle));
                stateChanger.ChangeState<HiveMindMoveEnemiesState>();
            }

            if (_context.SlackTime >= _config.SlackTime) 
                stateChanger.ChangeState<HiveMindAttackState>();
        }
    }
}