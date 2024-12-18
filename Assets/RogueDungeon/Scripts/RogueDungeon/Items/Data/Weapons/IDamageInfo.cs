namespace RogueDungeon.Items.Weapons
{
    public interface IDamageInfo
    {
        float Damage { get; }
        DamageType Type { get; }
    }
}