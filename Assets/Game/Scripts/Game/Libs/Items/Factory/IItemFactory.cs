namespace Game.Libs.Items.Factory
{
    public interface IItemFactory
    {
        IItem Create(string id);
    }
}