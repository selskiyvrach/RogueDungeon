namespace RogueDungeon.Player.Commands
{
    public interface ICommandsProvider
    {
        public bool HasCommand(Command command);
        public bool HasCommand(Command command, out float heldDuration);
    }
}