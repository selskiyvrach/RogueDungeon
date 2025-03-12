using Common.Animations;

namespace RogueDungeon.Enemies.States
{
    public class EnemyIdleState : EnemyState
    {
        private readonly Enemy _enemy;
        private readonly EnemyStateConfig _config;
        protected override bool IsLooping => true;

        public EnemyIdleState(EnemyStateConfig config, IAnimation animation, Enemy enemy) : base(config, animation)
        {
            _config = config;
            _enemy = enemy;
        }
    }
}