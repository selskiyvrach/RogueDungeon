namespace RogueDungeon.Items.Model
{
    public interface IAttackItemWielder : IItemTransitionsLockedProvider, IItemInputKeyProvider, IStaminaConsumingItemWielder
    {
        bool IsAttackInUncancellableState { set; }
        bool CanAttack { get; }
        void PerformAttack(IWeapon weapon);
    }
}