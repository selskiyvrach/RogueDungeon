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
        private readonly IDraggedItemParent _draggedItemParent;
        private readonly IGraphicRaycaster _raycaster;
        private readonly IPresentersRegistry _registry;
        private readonly Camera _camera;
        
        private ContainerPresenter _container;
        private ItemPresenter _item;
        private ProjectionData _projection;

        public DragItemState(Camera camera, IInventoryInput input, IDraggedItemParent draggedItemParent, IGraphicRaycaster raycaster, IPresentersRegistry registry)
        {
            _camera = camera;
            _input = input;
            _draggedItemParent = draggedItemParent;
            _raycaster = raycaster;
            _registry = registry;
        }

        public void Enter(ItemPresenter carriedItem)
        {
            carriedItem.ThrowIfNull();
            _item = carriedItem;
            _draggedItemParent.SetItem(_item.View);
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
            ScanForContainer();
            _draggedItemParent.SetScreenPosition(_input.ScreenPosition);
            if (_container == null)
            {
                _item.Projection.SetPosition(_draggedItemParent.WorldPosition);
                _item.Projection.SetIsValid(false);
            }
            else
            {
                _projection = _container.GetProjection(_item.Model, _camera, _input.ScreenPosition);
                _item.Projection.SetPosition(_projection.WorldPosition);
                _item.Projection.SetIsValid(_projection.Placement.IsPossible);
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
            if (_container != null && _projection.Placement.IsPossible)
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