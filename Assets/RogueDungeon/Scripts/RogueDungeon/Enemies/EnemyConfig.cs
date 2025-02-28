using RogueDungeon.Enemies.Attacks;
using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; } = 100f;
        [field: SerializeField] public EnemyInstaller Prefab { get; private set; }
        [field: SerializeField] public EnemyAttackActionConfig[] Attacks { get; private set; }
    }
}