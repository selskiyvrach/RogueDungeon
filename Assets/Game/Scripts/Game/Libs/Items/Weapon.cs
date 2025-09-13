namespace Game.Libs.Items
{
    public class Weapon : BlockingItem, IWeapon, ISlotableItem
    {
        private readonly IWeaponItemConfig _config;
        public float Damage => _config.Damage;
        public float PoiseDamage => _config.PoiseDamage;
        public float AttackStaminaCost => _config.AttackStaminaCost;
        public float AttackExecuteAnimationDuration => _config.AttackExecuteDuration;
        public float PrepareAttackAnimationDuration => _config.PrepareAttackDuration;
        public float AttackRecoveryAnimationDuration => _config.AttackRecoveryDuration;
        public float TransitionBetweenAttacksDuration => _config.TransitionBetweenAttacksDuration;
        public SlotCategory SlotCategory => SlotCategory.Handheld;

        public override BlockingTier BlockingTier => BlockingTier.Second;

        public Weapon(IWeaponItemConfig config, string id) : base(config, id) => 
            _config = config;
    }
}