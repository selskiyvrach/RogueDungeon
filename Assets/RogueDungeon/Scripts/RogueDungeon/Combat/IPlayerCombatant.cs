namespace RogueDungeon.Combat
{
    public interface IPlayerCombatant : ICombatTarget
    {
        PlayerDodgeState DodgeState { get; }
    }
}