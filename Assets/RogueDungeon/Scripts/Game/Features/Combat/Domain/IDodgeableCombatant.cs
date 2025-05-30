namespace Game.Features.Combat.Domain
{
    public interface IDodgeableCombatant
    {
        bool IsDodgingRight { get; }
        bool IsDodgingLeft { get; }
        void OnDodged();
    }
}