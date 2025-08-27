using System;
using System.Linq;
using Game.Libs.Items;
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
        
        private ContainerPresenter _container;
        private ItemPresenter _item;
        private ProjectionData _projection;

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
            _item = carriedItem;
            _input.OnMoved += OnCursorMoved;
            _input.OnPointerUp += OnPointerUp;
            
            ScanForContainer();
            Assert.IsNotNull(_container);
            UpdateItemPositionAndProjection();
            
            _container.ExtractItem(carriedItem);
            _item.DisplaySelected(true);
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
            _item.View.SetPosition(itemPos);
            if (_container == null)
            {
                _item.Projection.SetPosition(itemPos);
                _item.Projection.SetIsValid(true);
            }
            else
            {
                _projection = _container.GetProjection(_item.Model, _camera, _input.ScreenPosition);
                _item.Projection.SetPosition(_projection.WorldPosition);
                _item.Projection.SetIsValid(_projection.PlacementInquiry.IsPossible);
            }
        }

        private void ScanForContainer()
        {
            var containers = _raycaster.RaycastAll<IContainerView>(_input.ScreenPosition);
            Assert.IsFalse(containers.Count() > 1);
            var container = containers.FirstOrDefault();
            _container = container == null ? null : _registry.Containers.First(n => n.View == container);
        }

        private void OnPointerUp()
        {
            if (_container == null)
            {
                throw new NotImplementedException();
                Exit();
                Mediator.StopCarryingItem();
            }
            else if (_projection.PlacementInquiry.IsPossible)
            {
                _container.PlaceItem(_item, _projection.PlacementInquiry);
                _item.DisplaySelected(false);
                Exit();
                if(_projection.PlacementInquiry.ReplacedItem != null)
                    Mediator.StartCarryingItem(_registry.Items.First(n => n.Model == _projection.PlacementInquiry.ReplacedItem));
                else
                    Mediator.StopCarryingItem();
            }
            else
                _item.OnPlacementDenied();
        }

        public override void Dispose() => 
            Exit();
    }
}