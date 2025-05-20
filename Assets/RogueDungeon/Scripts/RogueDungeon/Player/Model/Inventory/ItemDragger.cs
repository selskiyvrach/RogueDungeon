using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Camera;
using RogueDungeon.Input;
using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public class ItemDragger
    {
        private readonly InputUnit _dragButton;
        private readonly IPlayerInput _input;
        private readonly IGameCamera _gameCamera;
        private readonly IEnumerable<ItemContainer> _containers;
        private readonly RectTransform _draggableSpaceRect;
        
        private ICommand _extractItemCommand;
        private ItemView _draggedItem;
        private ItemView _hoveredItem;
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
            if (_draggedItem != null)
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
                _draggedItem.SetPosition(worldPoint);
        }

        private void DetectHoveredContainer()
        {
            foreach (var container in _containers)
            {
                if (!container.TryProjectItem(_draggedItem, _input.CursorPos, _gameCamera.Camera, out var insertItemCommand)) 
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

                if (item != _hoveredItem)
                {
                    _hoveredItem?.SetIsBeingPointedAt(false);
                    _hoveredItem = item;
                    _hoveredItem.SetIsBeingPointedAt(true);
                }

                _extractItemCommand = extractItemCommand;
                return;
            }
            _hoveredItem?.SetIsBeingPointedAt(false);
            _hoveredItem = null;
        }

        private void PutItemDown()
        {
            if(_draggedItem == null)
                return;
            
            if(_insertItemCommand == null)
                return;
            
            _extractItemCommand = null;
            _draggedItem.SetIsBeingDragged(false);
            _insertItemCommand.Execute();
            _draggedItem = null;
            _insertItemCommand = null;
        }

        private void PickItemUp()
        {
            if(_hoveredItem == null)
                return;
            _extractItemCommand.Execute();
            _draggedItem = _hoveredItem;
            UnhoverItem();
            _draggedItem.SetParent(_draggableSpaceRect);
            _draggedItem.SetCellSize(30);
            _draggedItem.SetIsBeingDragged(true);
            DetectHoveredContainer();
        }

        private void UnhoverItem()
        {
            _hoveredItem?.SetIsBeingPointedAt(false);
            _hoveredItem = null;
        }
    }
}