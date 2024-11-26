namespace RogueDungeon.PlayerInputCommands
{
    public interface IPlayerInput
    {
        public bool HasCommand(Command command);
        public bool HasCommand(Command command, out float heldDuration);
        void ConsumeCommandIfCurrent(Command command);
    }
}