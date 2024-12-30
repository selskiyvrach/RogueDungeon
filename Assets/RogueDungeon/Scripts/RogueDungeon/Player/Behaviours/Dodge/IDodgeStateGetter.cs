namespace RogueDungeon.Player.Behaviours.Dodge
{
    public interface IDodgeStateGetter
    {
        DodgeState DodgeState { get; }
    }
}