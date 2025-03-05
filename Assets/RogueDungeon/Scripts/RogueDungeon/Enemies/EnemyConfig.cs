using RogueDungeon.Enemies.MoveSet;
using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; } = 100f;
        [field: SerializeField] public EnemyInstaller Prefab { get; private set; }
        [field: SerializeField] public EnemyMoveSetConfig MoveSet { get; private set; }
    }
}