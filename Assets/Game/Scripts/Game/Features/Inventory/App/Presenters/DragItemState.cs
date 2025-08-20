using Game.Libs.Items;
using Game.Libs.Items.Configs;
using Libs.Utils.DotNet;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Features.Inventory.App.Presenters
{
    public class DragItemState : MediatorState
    {
        private readonly Camera _camera;
        private readonly IProjectionView _projectionView;
        private readonly IItemConfigsRepository _itemRepository;
        
        private ContainerPresenter _container;
        private ItemPresenter _item;
        private bool _isPlacementPossible;

        public DragItemState(Camera camera, IProjectionView projectionView, IItemConfigsRepository itemRepository)
        {
            _camera = camera;
            _projectionView = projectionView;
            _itemRepository = itemRepository;
        }

        public void Enter(ItemPresenter carriedItem)
        {
            carriedItem.ThrowIfNull();
            _item = carriedItem;
            _item.DisplayBeingDragged();
            _projectionView.SetSprite(((ItemConfig)_itemRepository.GetItemConfig(_item.Model.TypeId)).Sprite);
        }

        public void OnCursorMoved(Vector2 screenPosition)
        {
            var projection = _container.GetProjection(_item.Model, _camera, screenPosition);
            _projectionView.SetPosition(projection.WorldPosition);
            _projectionView.SetIsValid(projection.Placement.IsPossible);
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
            Mediator.StopCarryingItem();
        }
    }
}