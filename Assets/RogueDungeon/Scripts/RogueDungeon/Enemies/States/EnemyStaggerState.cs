using Common.Animations;

namespace RogueDungeon.Enemies.States
{
    public class EnemyStaggerState : EnemyState
    {
        private readonly Enemy _enemy;
        
        protected EnemyStaggerState(EnemyStateConfig config, IAnimation animation, Enemy enemy) : base(config, animation) => 
            _enemy = enemy;

        public override void Exit()
        {
            base.Exit();
            _enemy.Poise.Refill();
        }
    }
}