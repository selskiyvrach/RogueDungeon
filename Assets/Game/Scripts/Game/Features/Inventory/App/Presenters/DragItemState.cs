using System.Linq;
using Game.Features.Inventory.Domain;
using Libs.Utils.DotNet;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Features.Inventory.App.Presenters
{
    public class DragItemState : MediatorState
    {
        private readonly IInventoryInput _input;
        private readonly IDraggableArea _draggableArea;
        private readonly IGraphicRaycaster _raycaster;
        private readonly IPresentersRegistry _registry;
        private readonly Camera _camera;
        
        private ContainerPresenter _currentContainer;
        private ItemPresenter _currentItem;
        private ProjectionData _projection;
        
        private ContainerPresenter _originContainer;
        private PositionNormalized _originPlacement;

        public DragItemState(Camera camera, IInventoryInput input, IDraggableArea draggableArea, IGraphicRaycaster raycaster, IPresentersRegistry registry)
        {
            _camera = camera;
            _input = input;
            _draggableArea = draggableArea;
            _raycaster = raycaster;
            _registry = registry;
        }

        public void Enter(ItemPresenter carriedItem)
        {
            carriedItem.ThrowIfNull();
            _currentItem = carriedItem;
            _input.OnMoved += OnCursorMoved;
            _input.OnPointerUp += OnPointerUp;
            
            ScanForContainer();
            UpdateItemPositionAndProjection();
            
            _originContainer = _registry.Containers.First(n => n.Model.ContainsItem(carriedItem.Model));
            _originContainer.ExtractItem(carriedItem, out _originPlacement);
            _draggableArea.PlaceItemAsChild(_currentItem);
            _currentItem.DisplaySelected(true);
        }

        private void Exit()
        {
            _input.OnMoved -= OnCursorMoved;
            _input.OnPointerUp -= OnPointerUp;
        }

        private void OnCursorMoved()
        {
            ScanForContainer();
            UpdateItemPositionAndProjection();
        }

        private void UpdateItemPositionAndProjection()
        {
            var itemPos = _draggableArea.ScreenToWorldPoint(_input.ScreenPosition);
            _currentItem.View.SetPosition(itemPos);
            if (_currentContainer == null)
            {
                _currentItem.Projection.SetPosition(itemPos);
                _currentItem.Projection.SetIsValid(true);
            }
            else
            {
                _projection = _currentContainer.GetProjection(_currentItem.Model, _camera, _input.ScreenPosition);
                _currentItem.Projection.SetPosition(_projection.WorldPosition);
                _currentItem.Projection.SetIsValid(_projection.PlacementProspect.IsPossible);
            }
        }

        private void ScanForContainer()
        {
            var containers = _raycaster.RaycastAll<IContainerView>(_input.ScreenPosition);
            Assert.IsFalse(containers.Count() > 1);
            var container = containers.FirstOrDefault();
            if (container == null)
            {
                _currentContainer = null;
                return;
            }
            var newContainer = _registry.Containers.First(n => n.View == container);
            if(newContainer == _currentContainer)
                return;
            _currentContainer = newContainer;
            _currentItem.View.SetCellSize(_currentContainer.View.CellSize);
        }

        private void OnPointerUp()
        {
            _currentItem.View.ProjectionView.SetIsValid(true);
            if (_currentContainer == null || !_projection.PlacementProspect.IsPossible)
            {
                _originContainer.Model.PlaceItem(_currentItem.Model, _originPlacement);
                if(_originContainer.Model.Id == ContainerId.Ground0)
                    _currentItem.PlayDroppedAnimation();
            }
            else
            {
                if (_projection.PlacementProspect.ReplacedItem is { } replaced && 
                    _originContainer.Model.GetItemPlacementProspect(replaced, _originPlacement) is { IsPossible : true } placement)
                {
                    _currentContainer.Model.RemoveItem(replaced);
                    _originContainer.Model.PlaceItem(replaced, placement.Position);
                    if(_originContainer.Model.Id == ContainerId.Ground0)
                        _registry.Items.First(n => n.Model == replaced).PlayDroppedAnimation();
                }
                _currentContainer.Model.PlaceItem(_currentItem.Model, _projection.PlacementProspect.Position);
                if(_currentContainer.Model.Id == ContainerId.Ground0)
                    _currentItem.PlayDroppedAnimation();
            }
            
            _currentItem.DisplaySelected(false);
            Exit();
            Mediator.StopCarryingItem();
        }

        public override void Dispose() => 
            Exit();
    }
}