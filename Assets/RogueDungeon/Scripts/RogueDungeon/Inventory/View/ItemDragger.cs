using System.Collections.Generic;
using System.Linq;
using Camera;
using Input;
using Inventory.Shared;
using UnityEngine;

namespace Inventory.View
{
    public class ItemDragger
    {
        private readonly InputUnit _dragButton;
        private readonly IPlayerInput _input;
        private readonly IGameCamera _gameCamera;
        private readonly IEnumerable<ItemContainer> _containers;
        private readonly RectTransform _draggableSpaceRect;
        
        private ICommand _extractItemCommand;
        private InventoryItemView _draggedInventoryItem;
        private InventoryItemView _hoveredInventoryItem;
        private ItemContainer _hoveredContainer;
        private ICommand _insertItemCommand;

        public ItemDragger(IPlayerInput input, IGameCamera gameCamera, IEnumerable<ItemContainer> containers, RectTransform draggableSpaceRect)
        {
            _input = input;
            _gameCamera = gameCamera;
            _draggableSpaceRect = draggableSpaceRect;
            _containers = containers.ToArray();
            _dragButton = _input.GetKey(InputKey.DragItem);
        }

        public void Enable()
        {
            _input.OnCursorMoved += HandleCursorMoved;
            _dragButton.OnDown += PickItemUp;
            _dragButton.OnUp += PutItemDown;
        }

        public void Disable()
        {
            _input.OnCursorMoved -= HandleCursorMoved;
            _dragButton.OnDown -= PickItemUp;
            _dragButton.OnUp -= PutItemDown;
            _extractItemCommand?.Undo();
            _extractItemCommand = null;
            UnhoverItem();
        }

        private void HandleCursorMoved()
        {
            if (_draggedInventoryItem != null)
            {
                DragItem();
                DetectHoveredContainer();
            }
            else
                DetectHoveredItem();
        }

        private void DragItem()
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(_draggableSpaceRect, _input.CursorPos, _gameCamera.Camera) && 
               RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggableSpaceRect, _input.CursorPos, _gameCamera.Camera, out var worldPoint))
                _draggedInventoryItem.SetPosition(worldPoint);
        }

        private void DetectHoveredContainer()
        {
            foreach (var container in _containers)
            {
                if (!container.TryProjectItem(_draggedInventoryItem, _input.CursorPos, _gameCamera.Camera, out var insertItemCommand)) 
                    continue;
                
                _insertItemCommand = insertItemCommand;
                return;
            }
        }

        private void DetectHoveredItem()
        {
            foreach (var container in _containers)
            {
                if (!container.TryRaycastItem(_input.CursorPos, _gameCamera.Camera, out var item, out var extractItemCommand)) 
                    continue;

                if (item != _hoveredInventoryItem)
                {
                    _hoveredInventoryItem?.SetIsBeingPointedAt(false);
                    _hoveredInventoryItem = item;
                    _hoveredInventoryItem.SetIsBeingPointedAt(true);
                }

                _extractItemCommand = extractItemCommand;
                return;
            }
            _hoveredInventoryItem?.SetIsBeingPointedAt(false);
            _hoveredInventoryItem = null;
        }

        private void PutItemDown()
        {
            if(_draggedInventoryItem == null)
                return;
            
            if(_insertItemCommand == null)
                return;
            
            _extractItemCommand = null;
            _draggedInventoryItem.SetIsBeingDragged(false);
            _insertItemCommand.Execute();
            _draggedInventoryItem = null;
            _insertItemCommand = null;
        }

        private void PickItemUp()
        {
            if(_hoveredInventoryItem == null)
                return;
            _extractItemCommand.Execute();
            _draggedInventoryItem = _hoveredInventoryItem;
            UnhoverItem();
            _draggedInventoryItem.SetParent(_draggableSpaceRect);
            _draggedInventoryItem.SetCellSize(30);
            _draggedInventoryItem.SetIsBeingDragged(true);
            DetectHoveredContainer();
        }

        private void UnhoverItem()
        {
            _hoveredInventoryItem?.SetIsBeingPointedAt(false);
            _hoveredInventoryItem = null;
        }
    }
}