using Game.Libs.Items;

namespace Game.Features.Player.Domain.Movesets.Items.Interfaces
{
    public interface IAttackItemWielder : IItemTransitionsLockedProvider, IItemInputKeyProvider, IStaminaConsumingItemWielder
    {
        bool IsAttackInUncancellableState { set; }
        bool CanAttack { get; }
        void PerformAttack(IWeapon weapon);
    }
}