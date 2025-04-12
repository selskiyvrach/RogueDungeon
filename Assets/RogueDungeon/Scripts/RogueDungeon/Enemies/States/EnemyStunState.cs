using System;
using Common.Animations;
using RogueDungeon.Characters;

namespace RogueDungeon.Enemies.States
{
    public class EnemyStunState : EnemyState
    {
        private readonly Enemy _enemy;
        public event Action OnChanged;
        
        protected EnemyStunState(EnemyStateConfig config, IAnimation animation, Enemy enemy) : base(config, animation) => 
            _enemy = enemy;

        public override void Enter()
        {
            base.Enter();
            OnChanged?.Invoke();
        }

        public override void Exit()
        {
            base.Exit();
            _enemy.Poise.Refill();
            OnChanged?.Invoke();
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            OnChanged?.Invoke();
        }
    }
}