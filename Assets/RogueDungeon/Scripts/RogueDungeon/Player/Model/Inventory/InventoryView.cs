using System.Linq;
using Common.UtilsDotNet;
using RogueDungeon.Camera;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace RogueDungeon.Player.Model.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField, HideInInspector] private WorldInventoryAnimator _animator;
        [SerializeField, HideInInspector] private ItemContainer[] _containers;
        
        private Level _level;
        private ItemView _currentItem;
        private IGameCamera _camera;
        private IPlayerInput _input;
        private ItemContainer _lootArea;
        private IInventoryViewModel _inventory;
        private ItemContainer _currentItemContainer;

        public bool IsOpen => _animator.State == WorldInventoryAnimator.AnimatorState.Open;

        [Inject]
        public void Construct(IPlayerInput input, IGameCamera gameCamera, Level level, IInventoryViewModel inventory)
        {
            _inventory = inventory;
            _input = input;
            _level = level;
            _camera = gameCamera;
            _inventoryCanvas.worldCamera = _camera.Camera;
        }

        private void OnValidate()
        {
            _animator = GetComponent<WorldInventoryAnimator>();
            _containers = GetComponentsInChildren<ItemContainer>();
        }

        public void Unpack()
        {
            _lootArea = _level.CurrentRoom.Presenter.LootArea.GetComponentInChildren<ItemContainer>().ThrowIfNull();
            _animator.Unpack();
        }

        public void Pack() => 
            _animator.Pack();

        public void Tick(float timeDelta)
        {
            if (_animator.State is WorldInventoryAnimator.AnimatorState.Opening or WorldInventoryAnimator.AnimatorState.Closing)
            {
                _animator.Tick(timeDelta);
                return;
            }
            
            if(!IsOpen)
                return;
            
            if(_currentItem?.IsBeingDragged ?? false)
                ScanForItems();
            //
            // if(_currentItem != null)
            //     DragItem();
        }

        // private void DragItem()
        // {
        //     if (_input.IsUp(InputKey.DragItem))
        //     {
        //         _currentItem.SetIsBeingDragged(false);
        //         _placeItemOnReleased.Invoke(out var replacedItem);
        //         if(replacedItem != null)
        //             
        //         return;
        //     }
        //
        //     if (_input.IsDown(InputKey.DragItem))
        //     {
        //         _input.ConsumeInput(InputKey.DragItem);
        //         _placeItemOnReleased = _currentItemContainer.GetPlaceItemBackAction(_currentItem);
        //         
        //         _currentItem.SetIsBeingDragged(true);
        //         _currentItem.transform.SetParent(_inventoryCanvas.transform);
        //         _currentItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //     }
        //     
        //     if(RectTransformUtility.RectangleContainsScreenPoint(_inventoryRect, _input.CursorPos, _camera.Camera) && 
        //        RectTransformUtility.ScreenPointToWorldPointInRectangle(_inventoryRect, _input.CursorPos, _camera.Camera, out Vector3 uiWorldPoint))
        //         _currentItem.SetPosition(uiWorldPoint);
        //     
        //     if(_placeablePlaces.Any(TryProjectOnPlaceablePlace) || TryProjectOnPlaceablePlace(_lootArea))
        //         return;
        //
        //     _currentItem.SetProjectionPosition(_currentItem.WorldPosition);
        //     _currentItem.SetIsCurrentPositionLegal(false);
        // }

        // private bool TryProjectOnPlaceablePlace(ItemContainer place)
        // {
        //     if(!place.TryProjectItem(_currentItem, _input.CursorPos, out var worldPos, out var canBePlaced, out var replacedItem, out PlaceItemCommand placeItemCommand))
        //         return false;
        //     
        //     _currentItem.SetProjectionPosition(worldPos);
        //     _currentItem.SetIsCurrentPositionLegal(canBePlaced);
        //     _currentItem.SetCellSize(place.CellSize);
        //     if (canBePlaced)
        //     {
        //         _placeItemOnReleased = placeItemCommand;
        //         
        //     }
        //     return true;
        // }

        private void ScanForItems()
        {
            // ItemView newItem;
            // if (HasItem(_lootArea, out newItem))
            //     _currentItemContainer = _lootArea;
            // else
            // {
            //     foreach (var place in _placeablePlaces)
            //     {
            //         if(!HasItem(place, out newItem))
            //             continue;
            //         _currentItemContainer = place;
            //         break;
            //     }
            // }
            //
            // if(newItem == _currentItem)
            //     return;
            //
            // _currentItem?.SetIsBeingPointedAt(false);
            // _currentItem = newItem;
            // _currentItem?.SetIsBeingPointedAt(true);
        }

        // private bool HasItem(ItemContainer container, out ItemView item) => 
        //     (item = container.TryRaycastItem(_input.CursorPos)) != null;
    }
}