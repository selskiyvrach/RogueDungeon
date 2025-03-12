using Common.Unity;
using RogueDungeon.Enemies.MoveSet;
using UnityEngine;
using IInitializable = Common.Lifecycle.IInitializable;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Enemies
{
    public class Enemy : IInitializable, ITickable
    {
        private readonly EnemyStateMachine _stateMachine;
        private readonly EnemyConfig _config;
        private float _currentHealth;

        public EnemyPosition TargetablePosition { get; set; }
        public EnemyPosition OccupiedPosition { get; set; }
        
        public ITwoDWorldObject WorldObject { get; }
        public bool IsAlive => _currentHealth > 0;
        public bool IsReadyToBeDisposed { get; set; }
        public bool IsIdle => _stateMachine.CurrentState is EnemyIdleState;
        public EnemyStateConfig[] States => _config.OtherStates;
        
        public Enemy(EnemyConfig config, GameObject gameObject, EnemyStateMachine stateMachine)
        {  
            WorldObject = new TwoDWorldObject(gameObject);
            _config = config;
            _stateMachine = stateMachine;
            _currentHealth = _config.Health;
        }

        public void Tick(float deltaTime) => 
            _stateMachine.Tick(deltaTime);

        public void Initialize()
        {
            TargetablePosition = OccupiedPosition;
            _stateMachine.Initialize();
            _stateMachine.TryStartState(_config.BirthState);
        }

        public void Destroy() =>
            ((TwoDWorldObject)WorldObject).Destroy();

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if(!IsAlive)
                _stateMachine.TryStartState(_config.DeathState);
        }

        public void PerformMove(EnemyStateConfig config) => 
            _stateMachine.TryStartState(config);
    }
}