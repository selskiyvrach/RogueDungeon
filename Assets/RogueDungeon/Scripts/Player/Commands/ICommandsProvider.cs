namespace RogueDungeon.Player.Commands
{
    public interface ICommandsProvider
    {
        public bool HasCommand(Command command);
    }

    public interface ICommandsConsumer
    {
        void ConsumeCommandIfCurrent(Command command, bool logError = false);
    }
}