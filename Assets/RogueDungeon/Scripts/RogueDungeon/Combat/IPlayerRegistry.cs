namespace RogueDungeon.Combat
{
    public interface IPlayerRegistry
    {
        IPlayerCombatant Player { get; }
        void RegisterPlayer(IPlayerCombatant player);
        void UnregisterPlayer(IPlayerCombatant player);
    }
}