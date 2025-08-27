using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Features.Inventory.App.Presenters
{
    public class ScanForItemState : MediatorState
    {
        private readonly Camera _camera;
        private readonly IInventoryInput _input;
        private readonly IGraphicRaycaster _raycaster;
        private readonly PresentersRegistry _registry;
        
        private ItemPresenter _currentItem;

        public ScanForItemState(PresentersRegistry registry, IGraphicRaycaster raycaster, Camera camera, IInventoryInput input)
        {
            _registry = registry;
            _raycaster = raycaster;
            _camera = camera;
            _input = input;
        }

        public void Enter()
        {
            foreach (var item in _registry.Items) 
                item.EnableRaycasts(true);
            _input.OnMoved += OnCursorMoved;
            _input.OnPointerDown += OnPointerDown;
            OnCursorMoved();
        }

        private void Exit()
        {
            _input.OnMoved -= OnCursorMoved;
            _input.OnPointerDown -= OnPointerDown;
        }

        private void OnCursorMoved() 
        {
            var closestItem = RaycastItem(_input.ScreenPosition);
            if (closestItem == null)
            {
                if(_currentItem != null)
                    SetCurrentHoveredItem(null);
                return;
            }

            var newItem = _registry.Items.First(n => n.View == closestItem);
            if (newItem == _currentItem) 
                return;
            
            if(_currentItem != null)
                SetCurrentHoveredItem(null);
            SetCurrentHoveredItem(newItem);
        }

        private IItemView RaycastItem(Vector2 cursorPos)
        {
            var items = _raycaster.RaycastAll<IItemView>(cursorPos);
            var closestDistance = float.MaxValue;
            IItemView closestItem = null;
            foreach (var item in items)
            {
                var distance = Vector2.Distance(cursorPos, item.GetScreenPosition(_camera));
                if(distance > closestDistance)
                    continue;
                closestDistance = distance;
                closestItem = item;
            }

            return closestItem;
        }

        private void SetCurrentHoveredItem(ItemPresenter item)
        {
            if (item != null)
            {
                Assert.IsNull(_currentItem);
                Assert.IsNotNull(item);
                _currentItem = item;
                _currentItem.SetHovered(true);
            }
            else
            {
                Assert.IsNotNull(_currentItem);
                _currentItem.SetHovered(false);
                _currentItem = null;
            }
        }

        private void OnPointerDown()
        {
            if (_currentItem == null)
                return;
            
            foreach (var item in _registry.Items) 
                item.EnableRaycasts(false);

            Exit();
            Mediator.StartCarryingItem(_currentItem);
        }

        public override void Dispose() => 
            Exit();
    }
}