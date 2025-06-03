using Game.Libs.InGameResources;

namespace Game.Features.Player.Domain
{
    public interface IPlayerConfig
    {
        ResourceConfig Health { get; }
        RechargeableResourceConfig Stamina { get; }
        float PositionOffsetFromTileCenter { get; }
        float DoubleGripDamageBonus { get; }
        float DoubleGripBlockBonus { get; }
        float DeathAnimationDuration { get; }
        float DodgeDuration { get; }
        float MovementActionDuration { get; }
        float TurnDuration { get; }
        float TurnAroundDuration { get; }
        float IdleAnimationDuration { get; }
        float DodgeStaminaCost { get; }
        float BirthAnimationDuration { get; }
        float OpenInventoryDuration { get; }
    }
}