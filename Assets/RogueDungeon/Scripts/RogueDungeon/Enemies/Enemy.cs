using Common.Unity;
using RogueDungeon.Combat;
using RogueDungeon.Enemies.Attacks;
using UnityEngine;
using Behaviour = Common.Behaviours.Behaviour;

namespace RogueDungeon.Enemies
{
    public class Enemy : Behaviour, IEnemyCombatant
    {
        private readonly EnemyLifeCycleMoveSetBehaviour _lifeCycleMoveSetBehaviour;
        private readonly EnemyConfig _config;
        private float _currentHealth;

        public EnemyPosition CombatPosition { get; set; }
        public ITwoDWorldObject WorldObject { get; }
        public bool IsAlive => _currentHealth > 0;
        public AttackBehaviour AttackBehaviour { get; }
        
        public Enemy(EnemyConfig config, GameObject gameObject, EnemyLifeCycleMoveSetBehaviour lifeCycleMoveSetBehaviour)
        {  
            WorldObject = new TwoDWorldObject(gameObject);
            _lifeCycleMoveSetBehaviour = lifeCycleMoveSetBehaviour;
            _config = config;
            _currentHealth = _config.Health;
        }

        public override void Enable()
        {
            base.Enable();
            _lifeCycleMoveSetBehaviour.Enable();
        }

        public override void Disable()
        {
            base.Disable();
            _lifeCycleMoveSetBehaviour.Disable();
        }

        public void Destroy() =>
            ((TwoDWorldObject)WorldObject).Destroy();

        public void TakeDamage(float damage) => 
            _currentHealth -= damage;
    }
}