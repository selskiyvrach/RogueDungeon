using System;
using Libs.Pools;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using IPoolable = Libs.Pools.IPoolable;
using Libs_Pools_IPoolable = Libs.Pools.IPoolable;

namespace Game.Features.Inventory.View
{
    public class SlotItemContainer : ItemContainer
    {
        private static readonly Pool<ExtractItemCommand> ExtractCommandsPool = new(() => new ExtractItemCommand(), 1);
        private static readonly Pool<InsertItemCommand> InsertCommandsPool = new(() => new InsertItemCommand(), 1);
        private Poolable<ExtractItemCommand>? _lastExtractCommand;
        private Poolable<InsertItemCommand>? _lastInsertCommand;
        
        [SerializeField] private float _cellSize = 30;
        [SerializeField] private SlotType _slotType;

        private InventoryItemView _inventoryItem;
        private IInventoryInfoProvider _inventory;

        protected override float CellSize => _cellSize;

        [Inject]
        private void Construct(IInventoryInfoProvider inventory) => 
            _inventory = inventory;

        private void Start()
        {
            if(_inventory.GetSlotItem(_slotType) is {} item)
                InsertItem(item);
        }

        protected override InventoryItemView RaycastItem(Vector3 screenPos, out ICommand extractItemCommand)
        {
            extractItemCommand = null;
            _lastExtractCommand?.Release();
            _lastExtractCommand = null;
            
            if(_inventoryItem == null)
                return null;
            
            _lastExtractCommand = ExtractCommandsPool.Get();
            _lastExtractCommand.Value.Item.Setup(this);
            extractItemCommand = _lastExtractCommand.Value.Item;
            return _inventoryItem;
        }

        protected override void GetItemProjection(InventoryItemView inventoryItem, Vector3 screenPos, out Vector3 projectionPos, out bool isPositionLegal, out ICommand placeItemCommand)
        {
            placeItemCommand = null;
            _lastInsertCommand?.Release();
            _lastInsertCommand = null;
            
            projectionPos = transform.position;
            isPositionLegal = inventoryItem.IsEquippableIntoSlotType(_slotType);
            
            if(!isPositionLegal)
                return;
            
            _lastInsertCommand = InsertCommandsPool.Get();
            _lastInsertCommand.Value.Item.Setup(inventoryItem, this);
            placeItemCommand = _lastInsertCommand.Value.Item;
        }

        private InventoryItemView ExtractItem()
        {
            var result = _inventoryItem;
            _inventoryItem.SetParent(null);
            _inventoryItem = null;
            return result;
        }

        private void InsertItem(InventoryItemView inventoryItem)
        {
            Assert.IsNull(_inventoryItem);
            Assert.IsNotNull(inventoryItem);
            
            _inventoryItem = inventoryItem;
            _inventoryItem.SetCellSize(CellSize);
            _inventoryItem.SetParent(transform);
            _inventoryItem.transform.localPosition = Vector3.zero;
        }
        
        private class InsertItemCommand : ICommand, Libs_Pools_IPoolable
        {
            private SlotItemContainer _container;   
            private InventoryItemView _inventoryItem;
            int IPoolable.RecyclesCount { get; set; }

            public void Setup(InventoryItemView inventoryItem, SlotItemContainer container)
            {
                _inventoryItem = inventoryItem;
                _container = container;
            }

            public void Execute() => 
                _container.InsertItem(_inventoryItem);

            public void Undo() => 
                throw new NotImplementedException();

            public void Reset()
            {
                _container = null;
                _inventoryItem = null;
            }
        }

        private class ExtractItemCommand : ICommand, Libs_Pools_IPoolable
        {
            private SlotItemContainer _container;
            private InventoryItemView _inventoryItem;
            int IPoolable.RecyclesCount { get; set; }

            public void Setup(SlotItemContainer container) => 
                _container = container;

            public void Execute() => 
                _inventoryItem = _container.ExtractItem();

            public void Undo() => 
                _container.InsertItem(_inventoryItem);

            void IPoolable.Reset()
            {
                _inventoryItem = null;
                _container = null;
            }
        }
    }
}