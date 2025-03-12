using Common.Animations;

namespace RogueDungeon.Enemies.States
{
    public class EnemyDeathState : EnemyState
    {
        private Enemy _enemy;

        public EnemyDeathState(EnemyStateConfig config, IAnimation animation, Enemy enemy) : base(config, animation) => 
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