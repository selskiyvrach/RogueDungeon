using Game.Libs.Items;
using Libs.Utils.DotNet;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemDraggerView
    {
        void SetItem(IItemView item);
        void SetItemScreenPosition(Vector2 position);
        void RemoveItem();
    }

    public interface IDraggerCursor
    {
        // I can raycast for container view and item view alike
        // I say to the selected item/container -> on cursor enter -> it gets propagated via the presenter to the mediator
    }

    public class DragItemState : MediatorState
    {
        private readonly IDragItemInput _input;
        private readonly IItemDraggerView _view;
        private readonly Camera _camera;
        private readonly IProjectionView _projectionView;
        private readonly IItemConfigsRepository _itemRepository;
        
        private ContainerPresenter _container;
        private ItemPresenter _item;
        private bool _isPlacementPossible;
        private ProjectionData _lastProjection;

        public DragItemState(Camera camera, IProjectionView projectionView, IItemConfigsRepository itemRepository, IDragItemInput input)
        {
            _camera = camera;
            _projectionView = projectionView;
            _itemRepository = itemRepository;
            // _view = view;
            _input = input;
        }

        public void Enter(ItemPresenter carriedItem)
        {
            carriedItem.ThrowIfNull();
            // container.getExtractItemCommand();
            _item = carriedItem;
            _item.DisplayBeingDragged();
            _view.SetItem(_item.View);
            _view.SetItemScreenPosition(_input.ScreenPosition);
            _projectionView.SetSprite(_itemRepository.GetItemSprite(_item.Model.TypeId));
        }

        public void OnCursorMoved(Vector2 screenPosition)
        {
            _view.SetItemScreenPosition(screenPosition);
            _lastProjection = _container.GetProjection(_item.Model, _camera, screenPosition);
            _projectionView.SetPosition(_lastProjection.WorldPosition);
            _projectionView.SetIsValid(_lastProjection.Placement.IsPossible);
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
            if (_container != null && _lastProjection.Placement.IsPossible)
            {
                // _container.
            }

            // execute place item command or undo of the extract one
            _item.DisplayNotBeingDragged();
            Mediator.StopCarryingItem();
        }
    }
}