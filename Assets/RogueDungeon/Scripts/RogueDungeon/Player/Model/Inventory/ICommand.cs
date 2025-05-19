namespace RogueDungeon.Player.Model.Inventory
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}