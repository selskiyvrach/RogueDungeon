namespace RogueDungeon.Input
{
    public interface IPlayerInput
    {
        bool HasInput(InputKey inputKey);
        void ConsumeInput(InputKey inputKey);
    }
}