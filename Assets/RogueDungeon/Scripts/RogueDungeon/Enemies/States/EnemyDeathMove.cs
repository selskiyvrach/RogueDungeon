using Common.Animations;

namespace RogueDungeon.Enemies.States
{
    public class EnemyDeathMove : EnemyMove
    {
        private Enemy _enemy;
        public override Priority Priority => Priority.Death;
        protected override float Duration => _enemy.Config.DeathAnimationDuration;

        public EnemyDeathMove(IAnimation animation, Enemy enemy, string id) : base(animation, id) => 
            _enemy = enemy;


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