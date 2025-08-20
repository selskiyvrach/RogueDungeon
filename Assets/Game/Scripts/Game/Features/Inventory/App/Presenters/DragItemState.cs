using Libs.Utils.DotNet;
using UnityEngine.Assertions;

namespace Game.Features.Inventory.App.Presenters
{
    public class DragItemState
    {
        private readonly Mediator _mediator;
        
        private ContainerPresenter _container;
        private ItemPresenter _item;
        private bool _isPlacementPossible;

        public DragItemState(Mediator mediator) => 
            _mediator = mediator;

        public void Enter(ItemPresenter carriedItem)
        {
            carriedItem.ThrowIfNull();
            _item = carriedItem;
            _item.DisplayBeingDragged();
        }

        public void OnCursorMoved()
        {
            _container.GetProjection();
        }

        public void OnContainerHovered(ContainerPresenter container)
        {
            Assert.IsNull(_container);
            _container = container;
        }

        public void OnContainerUnhovered(ContainerPresenter container)
        {
            Assert.IsNotNull(_container);
            Assert.AreEqual(_container, container);
            _container = null;
        }

        public void OnPointerUp()
        {
            // execute place item command or undo of the extract one
            _item.DisplayNotBeingDragged();
            _mediator.StopCarryingItem();
        }
    }
}