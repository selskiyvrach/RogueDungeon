using System;
using System.Collections.Generic;
using Common.Pools;
using Common.UtilsDotNet;
using RogueDungeon.Camera;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using IPoolable = Common.Pools.IPoolable;

namespace RogueDungeon.Player.Model.Inventory
{
    [RequireComponent(typeof(GraphicRaycaster)), 
     RequireComponent(typeof(RectTransform))]
    public class GridlessSpaceItemContainer : ItemContainer
    {
        [SerializeField] private float _cellSize = 30;
        [SerializeField, HideInInspector] private GraphicRaycaster _raycaster;
        
        private static readonly List<RaycastResult> RaycastHitsBuffer = new(15);
        private static readonly List<ItemView> RaycastedItemsBuffer = new(15);
        private static readonly Pool<ExtractItemCommand> ExtractCommandsPool = new(() => new ExtractItemCommand(), 1);
        private static readonly Pool<InsertItemCommand> InsertCommandsPool = new(() => new InsertItemCommand(), 1);
        
        private readonly HashSet<ItemView> _items = new();
        
        private Poolable<ExtractItemCommand>? _lastExtractCommand;
        private Poolable<InsertItemCommand>? _lastInsertCommand;
        private PointerEventData _pointer;
        private UnityEngine.Camera _camera;

        protected override float CellSize => _cellSize;

        protected override void OnValidate()
        {
            base.OnValidate();
            _raycaster = GetComponent<GraphicRaycaster>().ThrowIfNull();
        }
        
        [Inject]
        private void Construct(EventSystem eventSystem, IGameCamera gameCamera)
        {
            _camera = gameCamera.Camera;
            _pointer = new PointerEventData(eventSystem);
        }
        
        protected override ItemView RaycastItem(Vector3 screenPos, out ICommand extractItemCommand)
        {
            extractItemCommand = null;
            _lastExtractCommand?.Release();
            _lastExtractCommand = null;
            RaycastHitsBuffer.Clear();
            _pointer.Reset();
            
            _pointer.position = screenPos;
            _raycaster.Raycast(_pointer, RaycastHitsBuffer);
         
            RaycastedItemsBuffer.Clear();
            foreach (var hit in RaycastHitsBuffer)
            {
                if(hit.gameObject.GetComponent<ItemView>() is {} itemComponent)
                    RaycastedItemsBuffer.Add(itemComponent);
            }
        
            if (RaycastedItemsBuffer.Count == 0)
                return null;
        
            var item = RaycastedItemsBuffer[0];
            var closestDistance = float.PositiveInfinity;
            foreach (var bufferedItem in RaycastedItemsBuffer)
            {
                var itemPos = _camera.WorldToScreenPoint(bufferedItem.transform.position);
                var distance = Vector2.Distance(screenPos, itemPos);
                if (distance > closestDistance) 
                    continue;
            
                closestDistance = distance;
                item = bufferedItem;
            }
            _lastExtractCommand = ExtractCommandsPool.Get();
            _lastExtractCommand.Value.Item.Setup(item, this);
            extractItemCommand = _lastExtractCommand.Value.Item;
            return item;
        }

        protected override void GetItemProjection(ItemView item, Vector3 screenPos, out Vector3 projectionPos, out bool isPositionLegal, out ICommand placeItemCommand)
        {
            projectionPos = screenPos;
            isPositionLegal = true;
            _lastInsertCommand?.Release();
            _lastInsertCommand = InsertCommandsPool.Get();
            _lastInsertCommand.Value.Item.Setup(this, item, screenPos);
            placeItemCommand = _lastInsertCommand.Value.Item;
        }

        private void ExtractItem(ItemView item)
        {
            if (!_items.Remove(item))
                throw new InvalidOperationException();
            item.SetParent(null);
        }

        private void InsertItem(ItemView item, Vector3 localPosition)
        {
            if(!_items.Add(item))
                throw new InvalidOperationException();
            item.SetParent(transform);
            item.transform.localPosition = localPosition;
        }

        private class ExtractItemCommand : ICommand, IPoolable
        {
            private ItemView _item;
            private GridlessSpaceItemContainer _container;
            private Vector3 _localPosition;
            
            int IPoolable.RecyclesCount { get; set; }

            public void Setup(ItemView item, GridlessSpaceItemContainer container)
            {
                _item = item;
                _container = container;
            }

            public void Execute()
            {
                _localPosition = _item.transform.localPosition;
                _container.ExtractItem(_item);
            }

            public void Undo() => 
                _container.InsertItem(_item, _localPosition);

            void IPoolable.Reset()
            {
                _container = null;
                _item = null;
                _localPosition = Vector3.zero;
            }
        }

        private class InsertItemCommand : ICommand, IPoolable
        {
            private GridlessSpaceItemContainer _container;
            private ItemView _item;
            private Vector3 _worldPoint;
            
            int IPoolable.RecyclesCount { get; set; }

            public void Setup(GridlessSpaceItemContainer container, ItemView item, Vector3 worldPoint)
            {
                _container = container;
                _item = item;
                _worldPoint = worldPoint;
            }

            public void Execute() => 
                _container.InsertItem(_item, _worldPoint);

            public void Undo() => 
                throw new NotImplementedException();

            void IPoolable.Reset()
            {
                _item = null;
                _container = null;
                _worldPoint = default;
            }
        }
    }
}