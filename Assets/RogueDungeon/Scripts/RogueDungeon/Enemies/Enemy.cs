using RogueDungeon.Combat;
using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class Enemy : Common.Behaviours.Behaviour, IEnemyCombatant
    {
        private readonly EnemyLifeCycleMoveSetBehaviour _lifeCycleMoveSetBehaviour;
        private readonly EnemyConfig _config;
        private float _currentHealth;

        public EnemyPosition CombatPosition { get; set; }
        public GameObject GameObject { get; }
        public bool IsAlive => _currentHealth > 0;
        
        public Enemy(EnemyConfig config, GameObject gameObject, EnemyLifeCycleMoveSetBehaviour lifeCycleMoveSetBehaviour)
        {  
            GameObject = gameObject;
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
            Object.Destroy(GameObject);

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (!IsAlive)
            {
            }
        }
    }
}