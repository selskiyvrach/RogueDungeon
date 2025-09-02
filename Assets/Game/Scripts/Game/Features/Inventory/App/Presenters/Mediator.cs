using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public class Mediator : IInitializable
    {
        private readonly DragItemState _dragItemState;
        private readonly ScanForItemState _scanForItemState;
        
        public Mediator(DragItemState dragItemState, ScanForItemState scanForItemState)
        {
            _dragItemState = dragItemState;
            _scanForItemState = scanForItemState;
        }

        public void Initialize()
        {
            _scanForItemState.Init(this);
            _dragItemState.Init(this);
            _scanForItemState.Enter();
        }

        public void StartCarryingItem(ItemPresenter itemInQuestion) => 
            _dragItemState.Enter(itemInQuestion);

        public void StopCarryingItem() => 
            _scanForItemState.Enter();
    }
}