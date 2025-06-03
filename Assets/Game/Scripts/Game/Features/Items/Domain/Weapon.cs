namespace Game.Features.Items.Domain
{
    public class Weapon : BlockingItem, IWeapon
    {
        private readonly IWeaponItemConfig _config;
        public float Damage => _config.Damage;
        public float PoiseDamage => _config.PoiseDamage;
        public float AttackStaminaCost => _config.AttackStaminaCost;
        public float AttackExecuteAnimationDuration => _config.AttackExecuteDuration;
        public float PrepareAttackAnimationDuration => _config.PrepareAttackDuration;
        public float AttackRecoveryAnimationDuration => _config.AttackRecoveryDuration;
        public float TransitionBetweenAttacksDuration => _config.TransitionBetweenAttacksDuration;
        
        public override BlockingTier BlockingTier => BlockingTier.Second;
        public EquipmentType EquipmentType => EquipmentType.Handheld;

        public Weapon(IWeaponItemConfig config) : base(config) => 
            _config = config;
    }
}