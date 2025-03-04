namespace RogueDungeon.Player.Behaviours.Dodge
{
    public interface IDodger
    {
        PlayerDodgeState DodgeState { get; set; }
    }
}