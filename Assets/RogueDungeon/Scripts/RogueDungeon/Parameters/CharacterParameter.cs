namespace RogueDungeon.Parameters
{
    public enum CharacterParameter
    {
        None,
        Health = 100,
        HealthBonus = 150,
    }

    public enum Timings
    {
        None = 0,
        
        AttackPrepareDuration = 100,
        AttackExecuteDuration = 101,
        AttackFinishDuration = 102,
        AttackSpeedBonus = 150,
        
        DodgeDuration = 200,
    }
}