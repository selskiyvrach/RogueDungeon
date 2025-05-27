using RogueDungeon.Items.Model.Configs;
using Zenject;

namespace RogueDungeon.Items.Model
{
    public class Weapon : HandheldItem, IWeapon
    {
        private readonly WeaponConfig _config;
        public float Damage => _config.Damage;
        public float PoiseDamage => _config.PoiseDamage;
        public float AttackStaminaCost => _config.AttackStaminaCost;
        public BlockingTier BlockingTier => BlockingTier.Second;
        public float BlockStaminaCostMultiplier => _config.BlockStaminaCostMultiplier;
        public EquipmentType EquipmentType => EquipmentType.Handheld;

        public Weapon(WeaponConfig config) : base(config) => 
            _config = config;
    }

    public interface IWeapon : IBlockingItem
    {
        float Damage { get; }
        float PoiseDamage { get; }
        float AttackStaminaCost { get; }
    }
}