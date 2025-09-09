namespace Game.Features.Combat.Domain.Enemies.Moves
{
    public enum Priority
    {
        None,
        Idle = 1,
        
        Attack = 100,

        Move = 800,
        
        Stagger = 900,
        Birth = 998,
        Death = 999,
    }
}