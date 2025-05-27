using System;
using System.Linq;
using Characters;
using Common.Animations;
using Common.MoveSets;
using Enemies.States;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public static class MoveNames
    {
        public const string IDLE = "idle";
        public const string STAGGER = "stagger";
        public const string BIRTH = "birth";
        public const string DEATH = "death";
        public const string MOVE = "move";
        public const string ATTACK_LEFT = "attack_left";
        public const string ATTACK_RIGHT = "attack_right";
        public const string ATTACK_MIDDLE = "attack_middle";
    }

    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyInstaller Prefab { get; private set; }
        [field: SerializeField, InlineProperty] public ResourceConfig Health { get; private set; }
        [field: SerializeField, InlineProperty] public RechargeableResourceConfig Poise { get; private set; }
        [field: SerializeField, InlineProperty] public AnimationConfigPicker HitImpactAnimation { get; private set; }
        [field: SerializeField] public float ChillTime { get; private set; } = 1;
        [field: SerializeField] public Vector2Int FrontLineComboLength { get; private set; } = new(2, 3);
        // idle/stagger and others are potentially different, since they will use spritesheets of the enemy
        // so we need an animation picker to mix transform/spritesheed animations for now
        [field: HideLabel, BoxGroup(nameof(IdleState)), SerializeField] public AnimationConfigPicker IdleState { get; private set; }
        [field: HideLabel, BoxGroup(nameof(StaggerState)), SerializeField] public AnimationConfigPicker StaggerState { get; private set; }
        [field: HideLabel, BoxGroup(nameof(BirthState)), SerializeField] public AnimationConfigPicker BirthState { get; private set; }
        [field: HideLabel, BoxGroup(nameof(DeathState)), SerializeField] public AnimationConfigPicker DeathState { get; private set; }
        [field: HideLabel, BoxGroup(nameof(MoveState)), SerializeField] public AnimationConfigPicker MoveState { get; private set; }
        [field: SerializeField] public EnemyAttackMoveConfig[] Attacks { get; private set; }
        [field: SerializeField] public float IdleAnimationDuration { get; private set; } = 1;
        [field: SerializeField] public float StaggerDuration { get; private set; } = 2;
        [field: SerializeField] public float BirthAnimationDuration { get; private set; } = 1;
        [field: SerializeField] public float ChangePositionDuration { get; private set; } = .5f;
        [field: SerializeField] public float DeathAnimationDuration { get; private set; } = .25f;

        public MoveCreationArgs GetMoveArgs(string moveName) =>
            moveName switch
            {
                MoveNames.IDLE => GetArgs(moveName, typeof(EnemyIdleMove), IdleState.Config),
                MoveNames.STAGGER => GetArgs(moveName, typeof(EnemyStaggerMove), StaggerState.Config),
                MoveNames.BIRTH => GetArgs(moveName, typeof(EnemyBirthMove), BirthState.Config),
                MoveNames.DEATH => GetArgs(moveName, typeof(EnemyDeathMove), DeathState.Config),
                MoveNames.MOVE => GetArgs(moveName, typeof(EnemyMovementMove), MoveState.Config),
                MoveNames.ATTACK_LEFT or MoveNames.ATTACK_RIGHT or MoveNames.ATTACK_MIDDLE => new MoveCreationArgs(
                    moveName, typeof(EnemyAttackMove), Attacks.First(n => n.Name == moveName).Animation, Enumerable.Empty<TransitionPicker>(), Attacks.First(n => n.Name == moveName)),
                _ => throw new ArgumentOutOfRangeException(nameof(moveName), moveName, null)
            };
        
        private static MoveCreationArgs GetArgs(string moveName, Type moveType, AnimationConfig animation) => 
            new(moveName, moveType, animation, Enumerable.Empty<TransitionPicker>());
    }
}