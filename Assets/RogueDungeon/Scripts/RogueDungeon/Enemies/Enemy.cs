using RogueDungeon.Combat;
using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class Enemy : Common.Behaviours.Behaviour, IEnemyCombatant
    {
        private readonly EnemyLifeCycleMoveSetBehaviour _lifeCycleMoveSetBehaviour;
        private readonly EnemyConfig _config;

        public EnemyPosition CombatPosition { get; set; }
        public Transform Transform { get; }
        public bool IsAlive { get; } = true;
        
        public Enemy(EnemyConfig config, Transform transform, EnemyLifeCycleMoveSetBehaviour lifeCycleMoveSetBehaviour)
        {  
            Transform = transform;
            _lifeCycleMoveSetBehaviour = lifeCycleMoveSetBehaviour;
            _config = config;
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

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}