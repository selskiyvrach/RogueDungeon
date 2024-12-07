namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IAttackComboConfig
    {
        int Count { get; }
        IAttackTimingsProvider GetTimings(int attackIndex);
    }
}