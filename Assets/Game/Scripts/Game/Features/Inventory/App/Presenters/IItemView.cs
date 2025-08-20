using Game.Libs.UI;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemView : IHoverable, IIDable, IHoverDisplayable, IBeingDraggedDisplayable
    {
        void Setup(IItemInfo info);
    }
}