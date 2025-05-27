namespace Inventory.Shared
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}