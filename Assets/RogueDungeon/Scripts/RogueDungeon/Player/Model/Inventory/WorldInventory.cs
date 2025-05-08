using System.Linq;
using Common.UtilsDotNet;
using RogueDungeon.Camera;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventory : MonoBehaviour
    {
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField, HideInInspector] private WorldInventoryAnimator _animator;
        [SerializeField, HideInInspector] private PlaceablePlace[] _placeablePlaces;
        
        private Level _level;
        private WorldInventoryItem _currentItem;
        private IGameCamera _camera;
        private IPlayerInput _input;
        private PlaceablePlace _lootArea;
        public bool IsOpen => _animator.State == WorldInventoryAnimator.AnimatorState.Open;

        [Inject]
        public void Construct(IPlayerInput input, IGameCamera gameCamera, Level level)
        {
            _input = input;
            _level = level;
            _camera = gameCamera;
            _inventoryCanvas.worldCamera = _camera.Camera;
        }

        private void OnValidate()
        {
            _animator = GetComponent<WorldInventoryAnimator>();
            _placeablePlaces = GetComponentsInChildren<PlaceablePlace>();
        }

        public void Unpack()
        {
            _lootArea = _level.CurrentRoom.Presenter.LootArea.GetComponentInChildren<PlaceablePlace>().ThrowIfNull();
            _animator.Unpack();
        }

        public void Pack() =>
            _animator.Pack();

        public void Tick(float timeDelta)
        {
            if (_animator.State is WorldInventoryAnimator.AnimatorState.Opening
                or WorldInventoryAnimator.AnimatorState.Closing)
            {
                _animator.Tick(timeDelta);
                return;
            }
            
            if(!IsOpen)
                return;
            
            ScanForItems();
            DragItem();
        }

        private void DragItem()
        {
            if(_currentItem == null)
                return;

            if (_currentItem.IsBeingDragged && !_input.IsHeld(InputKey.DragItem)) 
                _currentItem.IsBeingDragged = false;

            if (!_currentItem.IsBeingDragged && _input.IsDown(InputKey.DragItem))
            {
                _input.ConsumeInput(InputKey.DragItem);
                _currentItem.IsBeingDragged = true;
            }

            if (!_currentItem.IsBeingDragged) 
                return;
    
            if(RectTransformUtility.RectangleContainsScreenPoint(_inventoryRect, _input.CursorPos, _camera.Camera) && 
               RectTransformUtility.ScreenPointToWorldPointInRectangle(_inventoryRect, _input.CursorPos, _camera.Camera, out Vector3 uiWorldPoint))
                _currentItem.Position = uiWorldPoint;
            
            if(_placeablePlaces.Any(TryProjectOnPlaceablePlace) || TryProjectOnPlaceablePlace(_lootArea))
                return;

            _currentItem.ProjectedPosition = _currentItem.Position;
            _currentItem.IsCurrentPositionLegal = false;
        }

        private bool TryProjectOnPlaceablePlace(PlaceablePlace place)
        {
            if(!place.TryProjectItem(_currentItem, _input.CursorPos, out var worldPos, out var canBePlaced))
                return false;
                
            _currentItem.ProjectedPosition = worldPos;
            _currentItem.IsCurrentPositionLegal = canBePlaced;
            return true;
        }

        private void ScanForItems()
        {
            if(_currentItem?.IsBeingDragged ?? false)
                return;
            
            var newItem = _lootArea.ScanForItem(_input.CursorPos) ?? _placeablePlaces.Select(n => n.ScanForItem(_input.CursorPos)).FirstOrDefault(n => n != null);
            
            if (newItem == null)
            {
                if (_currentItem == null) 
                    return;
                
                _currentItem.IsPointedAt = false;
                _currentItem.IsBeingDragged = false;
                _currentItem = null;
                return;
            }
            
            if(_currentItem == newItem)
                return;

            if (_currentItem != null)
            {
                _currentItem.IsPointedAt = false;
                _currentItem.IsBeingDragged = false;
                _currentItem = null;
            }

            _currentItem = newItem;
            _currentItem.IsPointedAt = true;
        }
    }
}