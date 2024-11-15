using RogueDungeon.Animations;
using RogueDungeon.Gameplay;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;
using UnityEngine;
using ITickable = Zenject.ITickable;

namespace RogueDungeon.Characters
{
    public abstract class Character : IDamageable, ITickable
    {
        private readonly StateMachine _behaviourStateMachine;
        protected readonly IEventBus<IAnimationEvent> AnimationEvents;
        
        protected readonly EntitiesRegistry EntitiesRegistry;
        public float AttackDamage { get; protected set; }
        public float MaxHealth { get; protected set; }
        public float CurrentHealth { get; protected set; }

        protected Character(IEventBus<IAnimationEvent> animationEvents, StateMachine behaviourStateMachine)
        {
            AnimationEvents = animationEvents;
            _behaviourStateMachine = behaviourStateMachine;
        }

        public void Enable() => 
            _behaviourStateMachine.Run();

        public void Disable() => 
            _behaviourStateMachine.Stop();

        public void TakeDamage(float damage) => 
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        
        public void Tick() => 
            _behaviourStateMachine.Tick();
    }
}