namespace Game.Features.Inventory.View
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}