using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Enemies.States
{
    public class EnemyIdleState : EnemyState
    {
        private readonly Enemy _enemy;
        private readonly EnemyStateConfig _config;
        private readonly EnemylifeCycleMoveSetContext _context;

        public EnemyIdleState(EnemyStateConfig config, IAnimator animator, EnemylifeCycleMoveSetContext context, Enemy enemy) : base(config, animator)
        {
            _config = config;
            _context = context;
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            // _context.EnemySpecificMoveSet.Enable();
        }

        public override void Exit()
        {
            base.Exit();
            // _context.EnemySpecificMoveSet.Disable();
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(!_enemy.IsAlive)
                stateChanger.ChangeState<EnemyDeathState>();
        }
    }
}