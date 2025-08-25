using System;
using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public class Mediator : IInitializable, IDisposable
    {
        private readonly ElementsRegistry _registry;
        private readonly IDragItemInput _input;
        private readonly DragItemState _dragItemState;
        private readonly ScanForItemState _scanForItemState;
        
        public ElementsRegistry Registry { get; }

        public Mediator(ElementsRegistry registry, IDragItemInput input, DragItemState dragItemState, ScanForItemState scanForItemState)
        {
            Registry = registry;
            _input = input;
            _dragItemState = dragItemState;
            _scanForItemState = scanForItemState;
        }

        public void Initialize()
        {
            _scanForItemState.Init(this);
            _dragItemState.Init(this);
            _input.OnPointerDown += _scanForItemState.OnPointerDown;
            _input.OnPointerUp += _dragItemState.OnPointerUp;
            _input.OnMoved += HandleCursorMoved;
            _scanForItemState.Enter();
        }

        private void HandleCursorMoved() => 
            _dragItemState.OnCursorMoved(_input.ScreenPosition);

        public void StartCarryingItem(ItemPresenter itemInQuestion) => 
            _dragItemState.Enter(itemInQuestion);
        
        public void StopCarryingItem() =>
            _scanForItemState.Enter();

        public void OnContainerHovered(ContainerPresenter container) => 
            _dragItemState.OnContainerHovered(container);

        public void OnContainerUnhovered(ContainerPresenter container) => 
            _dragItemState.OnContainerUnhovered(container);

        public void OnItemHovered(ItemPresenter item) => 
            _scanForItemState.OnItemHovered(item);

        public void OnItemUnhovered(ItemPresenter item) => 
            _scanForItemState.OnItemUnhovered(item);

        public void Dispose()
        {
            _input.OnPointerDown -= _scanForItemState.OnPointerDown;
            _input.OnPointerUp -= _dragItemState.OnPointerUp;
            _input.OnMoved -= HandleCursorMoved;
        }
    }
}