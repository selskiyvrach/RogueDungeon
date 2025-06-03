namespace Game.Features.Items.Domain
{
    public interface IWeapon : IBlockingItem
    {
        float Damage { get; }
        float PoiseDamage { get; }
        float AttackStaminaCost { get; }
        float AttackExecuteAnimationDuration { get; }
        float PrepareAttackAnimationDuration { get; }
        float AttackRecoveryAnimationDuration { get; }
        float TransitionBetweenAttacksDuration { get; }
    }
}