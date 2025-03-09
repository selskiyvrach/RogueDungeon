using Common.Animations;
using Common.Fsm;
using Common.Time;
using RogueDungeon.Enemies.HiveMind;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyDeathMove : EnemyMove
    {
        private Enemy _enemy;
        public EnemyDeathMove(EnemyMoveConfig config, IAnimation animation, Enemy enemy) : base(config, animation) => 
            _enemy = enemy;

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_enemy.IsAlive;
        
        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            if (!IsFinished || _enemy == null)
                return;
            
            _enemy.IsReadyToBeDisposed = true;
            _enemy = null;
        }
    }
}