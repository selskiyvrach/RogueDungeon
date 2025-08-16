namespace Game.Features.Inventory.Domain
{
    public interface IContainerVisitor
    {
        void Visit(GridSpaceItemContainer container); 
        void Visit(FreeSpaceItemContainer container);
        void Visit(SlotItemContainer container); 
    }
}