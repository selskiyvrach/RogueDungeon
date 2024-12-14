namespace RogueDungeon.PlayerInput
{
    public interface IInput
    {
        bool TryConsume(Input input);
    }
}