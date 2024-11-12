namespace RogueDungeon.Gameplay.InputCommands
{
    public interface ICommandsConsumer
    {
        void ConsumeCommandIfCurrent(Command command);
    }
}