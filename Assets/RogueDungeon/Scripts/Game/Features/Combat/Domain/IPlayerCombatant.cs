namespace Game.Features.Combat.Domain
{
    public interface IPlayerCombatant : IBlockingCombatant, IDamageableCombatant, IDodgeableCombatant, IDoubleGrippingCombatant, IStaminableCombatant
    {
    }
}