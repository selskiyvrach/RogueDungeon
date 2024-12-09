namespace RogueDungeon.Weapons
{
    public interface IAttackComboCountAndTimingsConfig
    {
        int Count { get; }
        IAttackTimingsProvider GetTimings(int attackIndex);
    }
}