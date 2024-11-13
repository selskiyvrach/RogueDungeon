using System;
using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;
using UnityEngine;
using ITickable = Zenject.ITickable;

namespace RogueDungeon.Gameplay
{
    public enum EnemyPosition
    {
        None,
        Middle,
        Left,
        Right,
    }

    public enum PlayerDodgeState
    {
        None,
        Left,
        Right
    }

    public enum EnemyAttackType
    {
        Center,
        Left,
        Right,
    }

    public interface IDamageable
    {
        void TakeDamage(float damage);
    }

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

    public interface IAnimationEvent
    {
    }

    public struct HitKeyframeEvent : IAnimationEvent
    {
    }

    public class Enemy : Character
    {
        public EnemyPosition Position { get; }
        public EnemyAttackType CurrentAttackDirection { get; }

        public Enemy(IEventBus<IAnimationEvent> animationEvents, StateMachine stateMachine) : base(animationEvents, stateMachine)
        {
        }

        protected IDamageable GetAttackTarget()
        {
            var player = EntitiesRegistry.Player; 
            return player.DodgeState switch
            {
                PlayerDodgeState.Left when CurrentAttackDirection == EnemyAttackType.Right => null,
                PlayerDodgeState.Right when CurrentAttackDirection == EnemyAttackType.Left => null,
                _ => player,
            };
        }
    }

    public class EntitiesRegistry
    {
        private readonly List<Enemy> _enemies;
        public Player Player { get; set; }
        public Enemy GetEnemyAtPosition(EnemyPosition position) =>
            _enemies.FirstOrDefault(n => n.Position == position);

        public bool HasEnemyAtPosition(EnemyPosition position) => 
            GetEnemyAtPosition(position) != null;

        public void RegisterEnemy(Enemy enemy)
        {
            if (HasEnemyAtPosition(enemy.Position))
                throw new Exception("Position is already occupied");
            _enemies.Add(enemy);
        }

        public void DeregisterEnemy(Enemy enemy) => 
            _enemies.Remove(enemy);
    }
}