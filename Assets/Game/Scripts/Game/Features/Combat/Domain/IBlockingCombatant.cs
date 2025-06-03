namespace Game.Features.Combat.Domain
{
    public interface IBlockingCombatant
    {
        float BlockStaminaCostMultiplier { get; }
        bool IsBlocking { get; }
        void OnBlocked();
    }
}