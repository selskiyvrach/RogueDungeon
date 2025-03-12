namespace RogueDungeon.Enemies.States
{
    public enum Priority
    {
        None,
        Idle = 1,
        
        Attack = 100,

        Move = 800,
        
        Stun = 900,
        Birth = 998,
        Death = 999,
    }
}