using Libs.Movesets;

namespace Game.Libs.Items
{
    public interface IItemConfigsRepository
    {
        IItemConfig GetItemConfig(string itemId);
        IMoveSetConfig GetItemMovesetConfig(string itemId);
    }
}