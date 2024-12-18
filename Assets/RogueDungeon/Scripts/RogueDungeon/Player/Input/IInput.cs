namespace RogueDungeon.Player.Input
{
    public interface IInput
    {
        bool TryConsume(Input input);
    }
}