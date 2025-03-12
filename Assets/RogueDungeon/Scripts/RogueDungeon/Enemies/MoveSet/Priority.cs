namespace RogueDungeon.Enemies.MoveSet
{
    public enum Priority
    {
        None,
        Idle = 1,
        
        Attack = 100,
        
        Stun = 900,
        Birth = 998,
        Death = 999,
    }
}