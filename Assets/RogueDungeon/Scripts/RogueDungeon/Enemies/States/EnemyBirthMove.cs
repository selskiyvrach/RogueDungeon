using Common.Animations;

namespace Enemies.States
{
    public class EnemyBirthMove : EnemyMove
    {
        private readonly Enemy _enemy;
        public override Priority Priority => Priority.Birth;
        protected EnemyBirthMove(IAnimation animation, Enemy enemy, string id) : base(animation, id)
        {
            _enemy = enemy;
        }

        protected override float Duration => _enemy.Config.BirthAnimationDuration;
    }
}