namespace RogueDungeon.Player.Behaviours.Dodge
{
    public interface IDodgeInput
    {
        bool TryConsume(DodgeInputCommand command);
    }
}