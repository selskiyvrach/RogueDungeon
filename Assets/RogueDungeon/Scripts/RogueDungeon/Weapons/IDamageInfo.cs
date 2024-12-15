namespace RogueDungeon.Behaviours.WeaponWielding
{
    public interface IDamageInfo
    {
        float Damage { get; }
        DamageType Type { get; }
    }
}