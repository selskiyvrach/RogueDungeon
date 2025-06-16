using Game.Libs.Combat;
using UnityEngine;

namespace Game.Features.Combat.Domain.Enemies
{
    public interface IEnemySpawner
    {
        Enemy Spawn(string id, EnemyPosition position, Transform parent);
    }
}