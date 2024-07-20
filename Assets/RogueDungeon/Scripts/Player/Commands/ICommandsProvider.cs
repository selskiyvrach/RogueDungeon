namespace RogueDungeon.Player.Commands
{
    public interface ICommandsProvider
    {
        public bool HasCommand(Command command);
    }
}