namespace Libs.Commands
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}