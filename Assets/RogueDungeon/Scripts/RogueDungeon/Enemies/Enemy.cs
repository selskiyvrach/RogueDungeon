using System;
using Common.Fsm;
using Common.Lifecycle;
using Common.Unity;
using RogueDungeon.Enemies.MoveSet;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Enemies
{
    public class Enemy : IInitializable, ITickable
    {
        private StateMachine _moveSetBehaviour;
        private readonly EnemyConfig _config;
        private float _currentHealth;

        public EnemyPosition TargetablePosition { get; set; }
        public EnemyPosition OccupiedPosition { get; set; }
        
        public ITwoDWorldObject WorldObject { get; }
        public bool IsAlive => _currentHealth > 0;
        public bool IsIdle { get; set; }
        public EnemyAttackMove[] Attacks => throw new NotImplementedException();

        public Enemy(EnemyConfig config, GameObject gameObject)
        {  
            WorldObject = new TwoDWorldObject(gameObject);
            _config = config;
            _currentHealth = _config.Health;
        }

        // creation step!
        public void SetBehaviour(StateMachine moveSetBehaviour)
        {
            Assert.IsNull(_moveSetBehaviour);
            Assert.IsNotNull(moveSetBehaviour);
            _moveSetBehaviour = moveSetBehaviour;
        }

        public void Tick(float deltaTime) => 
            _moveSetBehaviour.Tick(deltaTime);

        public void Initialize() => 
            _moveSetBehaviour.Initialize();

        public void Destroy() =>
            ((TwoDWorldObject)WorldObject).Destroy();

        public void TakeDamage(float damage) => 
            _currentHealth -= damage;

        public void PerformMove(EnemyAttackMove move)
        {
            throw new NotImplementedException();
        }
    }
}