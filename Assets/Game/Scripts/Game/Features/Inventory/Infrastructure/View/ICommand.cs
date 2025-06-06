namespace Game.Features.Inventory.Infrastructure.View
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}