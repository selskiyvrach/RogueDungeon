namespace RogueDungeon.Input
{
    public interface IUnit
    {
        bool Down { get; }
        bool Up { get; }
        bool Held { get; }
    }
}