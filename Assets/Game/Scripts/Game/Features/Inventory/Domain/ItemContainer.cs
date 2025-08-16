using Libs.Commands;

namespace Game.Features.Inventory.Domain
{
    public abstract class ItemContainer
    {
        public ContainerId Id { get; }

        protected ItemContainer(ContainerId id) => 
            Id = id;

        public abstract ICommand GetExtractItemCommand(string itemId, IExtractedItemCaretaker extractedItemCaretaker);
        public abstract void AcceptVisitor(IContainerVisitor visitor);
    }
}