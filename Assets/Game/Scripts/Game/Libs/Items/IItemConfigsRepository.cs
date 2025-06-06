namespace Game.Libs.Items
{
    public interface IItemConfigsRepository
    {
        IItemConfig GetItemConfig(string itemId);
    }
}