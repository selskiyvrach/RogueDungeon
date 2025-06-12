using Game.Features.Combat.Domain;
using UnityEngine;

namespace Game.Features.Combat.Infrastructure
{
    public class BattleFieldFactory : IBattleFieldFactory
    {
        private readonly Transform _enemiesParent;

        public BattleFieldFactory(Transform enemiesParent) => 
            _enemiesParent = enemiesParent;

        public Transform CreateBattleField(Vector2Int position, Vector2Int rotation)
        {
            _enemiesParent.position = new Vector3(position.x, 0, position.y);
            var angle = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
            angle = (angle + 360f) % 360f;
            _enemiesParent.rotation = Quaternion.Euler(0, angle, 0);
            return _enemiesParent;
        }
    }
}