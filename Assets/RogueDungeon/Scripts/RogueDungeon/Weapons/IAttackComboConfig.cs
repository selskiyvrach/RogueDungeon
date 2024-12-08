namespace RogueDungeon.Weapons
{
    public interface IAttackComboConfig
    {
        int Count { get; }
        IAttackTimingsProvider GetTimings(int attackIndex);
    }
}