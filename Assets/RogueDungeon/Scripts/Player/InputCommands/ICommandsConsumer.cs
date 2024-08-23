namespace RogueDungeon.Player.Commands
{
    public interface ICommandsConsumer
    {
        void ConsumeCommandIfCurrent(Command command);
    }
}