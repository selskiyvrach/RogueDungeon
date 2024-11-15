namespace RogueDungeon.PlayerInputCommands
{
    public interface ICommandsConsumer
    {
        void ConsumeCommandIfCurrent(Command command);
    }
}