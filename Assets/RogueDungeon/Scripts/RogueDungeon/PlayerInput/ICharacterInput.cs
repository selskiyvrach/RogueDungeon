namespace RogueDungeon.PlayerInput
{
    public interface ICharacterInput
    {
        public bool HasCommand(Command command);
        public bool HasCommand(Command command, out float heldDuration);
        void ConsumeCommandIfCurrent(Command command);
    }
}