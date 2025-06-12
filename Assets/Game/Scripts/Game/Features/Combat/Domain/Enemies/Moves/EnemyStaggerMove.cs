using System;
using Libs.Animations;

namespace Game.Features.Combat.Domain.Enemies
{
    public class EnemyStaggerMove : EnemyMove
    {
        private readonly Enemy _enemy;
        protected override float Duration => _enemy.Config.StaggerDuration;
        public override Priority Priority => Priority.Stagger;
        public event Action OnChanged;

        protected EnemyStaggerMove(IAnimation animation, Enemy enemy, string id) : base(animation, id) => 
            _enemy = enemy;

        public override void Enter()
        {
            base.Enter();
            OnChanged?.Invoke();
            _enemy.IsStunned = true;
        }

        public override void Exit()
        {
            base.Exit();
            _enemy.Poise.Refill();
            OnChanged?.Invoke();
            _enemy.IsStunned = false;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            OnChanged?.Invoke();
        }
    }
}