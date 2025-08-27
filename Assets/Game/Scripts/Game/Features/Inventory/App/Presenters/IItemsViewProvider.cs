using Game.Libs.Items;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemsViewProvider
    {
        IItemView GetView(IItem model);
    }
}