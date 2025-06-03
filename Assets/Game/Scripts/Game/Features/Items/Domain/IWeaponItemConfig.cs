namespace Game.Features.Items.Domain
{
    public interface IWeaponItemConfig : IBlockingItemConfig
    {
        float Damage { get; }
        float PoiseDamage { get; }
        float AttackStaminaCost { get; }
        float AttackExecuteDuration { get; }
        float PrepareAttackDuration { get; }
        float AttackRecoveryDuration { get; }
        float TransitionBetweenAttacksDuration { get; }
    }
}