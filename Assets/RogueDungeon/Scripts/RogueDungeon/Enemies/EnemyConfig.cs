using System;
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
        [field: SerializeField, InlineProperty] public AnimationConfigPicker HitImpactAnimation { get; private set; }
        [field: SerializeField] public EnemyInstaller Prefab { get; private set; }
        [field: SerializeField] public EnemyIdleConfig IdleState { get; private set; }
        [field: SerializeField] public EnemyStaggerConfig StaggerState { get; private set; }
        [field: SerializeField] public EnemyBirthConfig BirthState { get; private set; }
        [field: SerializeField] public EnemyDeathConfig DeathState { get; private set; }
        [field: SerializeField] public EnemyMovementConfig MoveState { get; set; }
        [field: SerializeField] public EnemyMoveConfig[] Moves { get; private set; }
        [field: SerializeField] public float ChillTime { get; private set; } = 1;
        [field: SerializeField] public Vector2Int FrontLineComboLength { get; private set; } = new(2, 3);
    }
}