using RogueDungeon.Combat;
using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class Enemy : IEnemyCombatant
    {
        private readonly EnemyConfig _config;

        public EnemyPosition Position { get; set; }
        public Transform Transform { get; }

        public Enemy(EnemyConfig config, Transform transform)
        {
            Transform = transform;
            _config = config;
        }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}