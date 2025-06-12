using Libs.Animations;

namespace Game.Features.Combat.Domain.Enemies
{
    public class EnemyBirthMove : EnemyMove
    {
        private readonly Enemy _enemy;
        public override Priority Priority => Priority.Birth;
        protected override float Duration => _enemy.Config.BirthAnimationDuration;

        protected EnemyBirthMove(IAnimation animation, Enemy enemy, string id) : base(animation, id) => 
            _enemy = enemy;
    }
}