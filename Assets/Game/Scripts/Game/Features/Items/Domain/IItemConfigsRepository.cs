namespace Game.Features.Items.Domain
{
    public interface IItemConfigsRepository
    {
        IItemConfig GetItemConfig(string itemId);
    }
}