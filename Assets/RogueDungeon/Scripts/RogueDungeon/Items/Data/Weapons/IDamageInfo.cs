namespace RogueDungeon.Items.Data.Weapons
{
    public interface IDamageInfo
    {
        float Damage { get; }
        DamageType DamageType { get; }
    }
}