using Game.Libs.Items;
using Libs.Utils.DotNet;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public class DragItemState : MediatorState
    {
        private readonly IInventoryInput _input;
        private readonly IDraggedItemParent _parent;
        private readonly Camera _camera;
        private readonly IProjectionView _projectionView;
        private readonly IItemConfigsRepository _itemRepository;
        
        private ContainerPresenter _container;
        private ItemPresenter _item;
        private ProjectionData _lastProjection;

        public DragItemState(Camera camera, IProjectionView projectionView, IItemConfigsRepository itemRepository, IInventoryInput input, IDraggedItemParent parent)
        {
            _camera = camera;
            _projectionView = projectionView;
            _itemRepository = itemRepository;
            _input = input;
            _parent = parent;
        }

        public void Enter(ItemPresenter carriedItem)
        {
            carriedItem.ThrowIfNull();
            _item = carriedItem;
            _parent.SetItem(_item.View);
            _projectionView.SetSprite(_itemRepository.GetItemSprite(_item.Model.TypeId));
            _input.OnMoved += OnCursorMoved;
            _input.OnPointerUp += OnPointerUp;
            OnCursorMoved();
        }

        private void Exit()
        {
            _input.OnMoved -= OnCursorMoved;
            _input.OnPointerUp -= OnPointerUp;
        }

        private void OnCursorMoved()
        {
            // scan for container
            
            _parent.SetScreenPosition(_input.ScreenPosition);
            _lastProjection = _container.GetProjection(_item.Model, _camera, _input.ScreenPosition);
            _projectionView.SetPosition(_lastProjection.WorldPosition);
            _projectionView.SetIsValid(_lastProjection.Placement.IsPossible);
        }

        private void OnPointerUp()
        {
            if (_container != null && _lastProjection.Placement.IsPossible)
            {
                // _container.
            }

            // execute place item command or undo of the extract one
            Exit();
            Mediator.StopCarryingItem();
        }

        public override void Dispose() => 
            Exit();
    }
}