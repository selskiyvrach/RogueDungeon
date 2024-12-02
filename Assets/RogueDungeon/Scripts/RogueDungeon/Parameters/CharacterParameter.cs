namespace RogueDungeon.Parameters
{
    public enum CharacterParameter
    {
        None,
        Health = 100,
    }

    public enum Timings
    {
        None = 0,
        // Attack
        AttackPrepareDuration = 100,
        AttackExecuteDuration = 101,
        AttackFinishDuration = 102,
        // Dodge
        DodgeDuration = 200,
    }
}