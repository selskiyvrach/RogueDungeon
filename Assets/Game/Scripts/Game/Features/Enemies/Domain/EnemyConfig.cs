using System;
using System.Linq;
using Game.Features.Enemies.Domain.Moves;
using Game.Libs.InGameResources;
using Libs.Animations;
using Libs.Movesets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Enemies.Domain
{
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField, InlineProperty] public ResourceConfig Health { get; private set; }
        [field: SerializeField, InlineProperty] public RechargeableResourceConfig Poise { get; private set; }
        [field: SerializeField, InlineProperty] public AnimationConfigPicker HitImpactAnimation { get; private set; }
        [field: SerializeField] public float ChillTime { get; private set; } = 1;
        [field: SerializeField] public Vector2Int FrontLineComboLength { get; private set; } = new(2, 3);
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
                MoveNames.IDLE => GetArgs(moveName, IdleState.Config),
                MoveNames.STAGGER => GetArgs(moveName, StaggerState.Config),
                MoveNames.BIRTH => GetArgs(moveName, BirthState.Config),
                MoveNames.DEATH => GetArgs(moveName, DeathState.Config),
                MoveNames.MOVE => GetArgs(moveName, MoveState.Config),
                MoveNames.ATTACK_LEFT or MoveNames.ATTACK_RIGHT or MoveNames.ATTACK_MIDDLE => new MoveCreationArgs(
                    moveName, Attacks.First(n => n.Name == moveName).Animation, Enumerable.Empty<TransitionPicker>()),
                _ => throw new ArgumentOutOfRangeException(nameof(moveName), moveName, null)
            };
        
        private static MoveCreationArgs GetArgs(string moveName, AnimationConfig animation) => 
            new(moveName, animation, Enumerable.Empty<TransitionPicker>());
    }
}