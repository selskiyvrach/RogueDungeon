namespace RogueDungeon.Behaviours.WeaponWielding
{
    public class Weapon : IWeaponInfo
    {
        private readonly WeaponConfig _config;
        AttackDirection[] IComboInfo.AttackDirectionsInCombo => _config.AttackDirectionsInCombo;
        float IItemInfo.Weight => _config.Weight;
        float IDamageInfo.Damage => _config.Damage;
        DamageType IDamageInfo.Type => _config.Type;
    }
}