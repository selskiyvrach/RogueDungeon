using Game.Libs.Items;

namespace Game.Features.Inventory.Domain
{
    public interface IExtractedItemCaretaker
    {
        void SetItem(IItem item);
        void RemoveItem(IItem item);
    }
}