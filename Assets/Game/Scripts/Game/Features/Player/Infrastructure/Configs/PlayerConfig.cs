using Game.Features.Player.Domain;
using Game.Features.Player.Domain.Behaviours.CommonMoveset;
using Game.Libs.InGameResources;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Player.Infrastructure.Configs
{
    public class PlayerConfig : ScriptableObject, IPlayerConfig
    {
        [field: SerializeField, InlineProperty] public ResourceConfig Health { get; private set; }
        [field: SerializeField, InlineProperty] public RechargeableResourceConfig Stamina { get; private set; }
        [field: SerializeField, Range(0.01f, 0.49f)] public float PositionOffsetFromTileCenter { get; private set; } = .35f;
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float DoubleGripDamageBonus { get; private set;} = 1.2f;
        [field: SerializeField] public float DoubleGripBlockBonus { get; private set;} = 1.2f;
        [field: SerializeField] public float DeathAnimationDuration { get; private set; } = 2f;
        [field: SerializeField] public float DodgeDuration { get; private set; }
        [field: SerializeField] public float MovementActionDuration { get; private set; } = .5f;
        [field: SerializeField] public float TurnDuration { get; private set; } = .5f;
        [field: SerializeField] public float TurnAroundDuration { get; private set; } = .75f;
        [field: SerializeField] public float IdleAnimationDuration { get; private set; } = 1f;
        [field: SerializeField] public float DodgeStaminaCost { get; private set; } = 10f;
        [field: SerializeField] public float BirthAnimationDuration { get; private set; } = 1f;
        [field: SerializeField] public float OpenInventoryDuration { get; private set; } = .5f;
        [field: SerializeField, HideLabel] public PlayerMoveSetConfig MoveSetConfig { get; private set; }
    }
}