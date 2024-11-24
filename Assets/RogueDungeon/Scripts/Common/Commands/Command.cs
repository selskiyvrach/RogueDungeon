namespace Common.Commands
{
    public abstract class Command : ICommand
    {
        public abstract void Execute();
    }
}