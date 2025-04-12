namespace RogueDungeon.Items
{
    public class Weapon : Item, IWeapon
    {
        private readonly WeaponConfig _config;
        public float Damage => _config.Damage;
        public float PoiseDamage => _config.PoiseDamage;
        public float AttackStaminaCost => _config.AttackStaminaCost;

        public Weapon(WeaponConfig config) : base(config) => 
            _config = config;
    }

    public interface IWeapon : IItem
    {
        float Damage { get; }
        float PoiseDamage { get; }
        float AttackStaminaCost { get; }
    }
}