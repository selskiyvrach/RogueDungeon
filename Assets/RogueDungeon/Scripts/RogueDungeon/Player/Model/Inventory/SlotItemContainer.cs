using System;
using Common.Pools;
using UnityEngine;
using UnityEngine.Assertions;
using IPoolable = Common.Pools.IPoolable;

namespace RogueDungeon.Player.Model.Inventory
{
    public class SlotItemContainer : ItemContainer
    {
        private static readonly Pool<ExtractItemCommand> ExtractCommandsPool = new(() => new ExtractItemCommand(), 1);
        private static readonly Pool<InsertItemCommand> InsertCommandsPool = new(() => new InsertItemCommand(), 1);
        private Poolable<ExtractItemCommand>? _lastExtractCommand;
        private Poolable<InsertItemCommand>? _lastInsertCommand;
        
        [SerializeField] private float _cellSize = 30;
        [SerializeField] private SlotType _slotType;

        private ItemView _item;
        protected override float CellSize => _cellSize;

        protected override ItemView RaycastItem(Vector3 screenPos, out ICommand extractItemCommand)
        {
            extractItemCommand = null;
            _lastExtractCommand?.Release();
            _lastExtractCommand = null;
            
            if(_item == null)
                return null;
            
            _lastExtractCommand = ExtractCommandsPool.Get();
            _lastExtractCommand.Value.Item.Setup(this);
            extractItemCommand = _lastExtractCommand.Value.Item;
            return _item;
        }

        protected override void GetItemProjection(ItemView item, Vector3 screenPos, out Vector3 projectionPos, out bool isPositionLegal, out ICommand placeItemCommand)
        {
            placeItemCommand = null;
            _lastInsertCommand?.Release();
            _lastInsertCommand = null;
            
            projectionPos = transform.position;
            isPositionLegal = item.IsEquippableIntoSlotType(_slotType);
            
            if(!isPositionLegal)
                return;
            
            _lastInsertCommand = InsertCommandsPool.Get();
            _lastInsertCommand.Value.Item.Setup(item, this);
            placeItemCommand = _lastInsertCommand.Value.Item;
        }

        private ItemView ExtractItem()
        {
            var result = _item;
            _item.SetParent(null);
            _item = null;
            return result;
        }

        private void InsertItem(ItemView item)
        {
            Assert.IsNull(_item);
            Assert.IsNotNull(item);
            
            _item = item;
            _item.SetParent(transform);
            _item.transform.localPosition = Vector3.zero;
        }
        
        private class InsertItemCommand : ICommand, IPoolable
        {
            private SlotItemContainer _container;   
            private ItemView _item;
            int IPoolable.RecyclesCount { get; set; }

            public void Setup(ItemView item, SlotItemContainer container)
            {
                _item = item;
                _container = container;
            }

            public void Execute() => 
                _container.InsertItem(_item);

            public void Undo() => 
                throw new NotImplementedException();

            public void Reset()
            {
                _container = null;
                _item = null;
            }
        }

        private class ExtractItemCommand : ICommand, IPoolable
        {
            private SlotItemContainer _container;
            private ItemView _item;
            int IPoolable.RecyclesCount { get; set; }

            public void Setup(SlotItemContainer container) => 
                _container = container;

            public void Execute() => 
                _item = _container.ExtractItem();

            public void Undo() => 
                _container.InsertItem(_item);

            void IPoolable.Reset()
            {
                _item = null;
                _container = null;
            }
        }
    }
}