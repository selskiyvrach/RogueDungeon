using RogueDungeon.Enemies.States;
using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; } = 100f;
        [field: SerializeField] public EnemyInstaller Prefab { get; private set; }
        [field: SerializeField] public EnemyIdleConfig IdleState { get; private set; }
        [field: SerializeField] public EnemyBirthConfig BirthState { get; private set; }
        [field: SerializeField] public EnemyDeathConfig DeathState { get; private set; }
        [field: SerializeField] public EnemyMoveConfig MoveState { get; set; }
        [field: SerializeField] public EnemyStateConfig[] OtherStates { get; private set; }
    }
}