using Common.Animations;
using RogueDungeon.Characters;
using RogueDungeon.Enemies.States;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField, InlineProperty] public ResourceConfig Health { get; private set; }
        [field: SerializeField, InlineProperty] public RechargeableResourceConfig Poise { get; private set; }
        [field: SerializeField, InfoBox("Speed at which a '0-1' scale of being ready to attack fills per second. 0 - never attacks, 1 - once per second, 2 - twice, e.t.c"), Range(0, 2)] public float Aggression { get; private set; } = 1f;
        [field: SerializeField, InlineProperty] public AnimationConfigPicker HitImpactAnimation { get; private set; }
        [field: SerializeField] public EnemyInstaller Prefab { get; private set; }
        [field: SerializeField] public EnemyIdleConfig IdleState { get; private set; }
        [field: SerializeField] public EnemyStaggerConfig StaggerState { get; private set; }
        [field: SerializeField] public EnemyBirthConfig BirthState { get; private set; }
        [field: SerializeField] public EnemyDeathConfig DeathState { get; private set; }
        [field: SerializeField] public EnemyMovementConfig MoveState { get; set; }
        [field: SerializeField] public EnemyMoveConfig[] Moves { get; private set; }
    }
}