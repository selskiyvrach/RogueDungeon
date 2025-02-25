using Common.Animations;
using Common.Fsm;
using RogueDungeon.Combat;

namespace RogueDungeon.Enemies
{
    public class EnemyBeingAliveState : EnemyLifeCycleState
    {
        private readonly IEnemyCombatant _enemy;
        private readonly EnemyLifeCycleConfig _config;
        private readonly EnemylifeCycleMoveSetContext _context;
        protected override AnimationData Animation => new(_config.BeingAliveAnimation.name, 1);

        public EnemyBeingAliveState(EnemyLifeCycleConfig config, IAnimator animator, EnemylifeCycleMoveSetContext context, IEnemyCombatant enemy) : base(config, animator)
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