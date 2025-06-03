namespace Game.Features.Combat.Domain
{
    public interface IDoubleGrippingCombatant
    {
        bool IsDoubleGrip { get; }
        float DoubleGripDamageBonus { get; }
    }
}