using System;
using System.Collections.Generic;
using System.Linq;
using Libs.GridSpace;
using Libs.Pools;
using Libs.Utils.DotNet;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IPoolable = Libs.Pools.IPoolable;
using Libs_Pools_IPoolable = Libs.Pools.IPoolable;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class GridSpaceItemContainer : ItemContainer
    {
        [SerializeField] private GridLayoutGroup _gridLayout;
        
        private static readonly Pool<ExtractItemCommand> ExtractItemCommandsPool = new(() => new ExtractItemCommand(), 1);
        private static readonly Pool<InsertItemCommand> InsertItemCommandsPool = new(() => new InsertItemCommand(), 1);

        private readonly HashSet<InventoryItemView> _items = new();

        private Poolable<ExtractItemCommand>? _lastExtractItemCommand;
        private Poolable<InsertItemCommand>? _lastInsertItemCommand;
        
        private GridSpace _gridSpace;
        private Camera _camera;
        private int _rows;
        private int _columns;
        protected override float CellSize => _gridLayout.cellSize.x;

        [Inject]
        public void Construct(Camera camera)
        {
            _camera = camera;
            _gridLayout.GetRowsAndColumns(out _rows, out _columns);
            _gridSpace = new GridSpace(_columns, _rows);
        }
        
        protected override InventoryItemView RaycastItem(Vector3 screenPos, out ICommand extractItemCommand)
        {
            extractItemCommand = null;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPos, _camera,  out var localPoint);
            LocalRectPosToCell(localPoint, out var cellColumn, out var cellRow);

            if(!_gridSpace.HasItem(cellColumn, cellRow, out var item))
                return null;

            var itemView = _items.First(n => n.Id == item.Id);
            
            _lastExtractItemCommand?.Release();
            _lastExtractItemCommand = ExtractItemCommandsPool.Get();
            _lastExtractItemCommand.Value.Item.Setup(this, itemView, item);
            extractItemCommand = _lastExtractItemCommand.Value.Item;
            
            return itemView;
        }

        private bool LocalRectPosToCell(Vector2 localPoint, out int cellColumn, out int cellRow)
        {
            localPoint += _rectTransform.sizeDelta / 2f;
            
            cellRow = Mathf.FloorToInt( (1 - localPoint.y / _rectTransform.sizeDelta.y) * _columns);
            cellColumn = Mathf.FloorToInt(localPoint.x / _rectTransform.sizeDelta.x * _rows);

            return _gridSpace.ContainedInSpace(cellColumn, cellRow);
        }

        protected override void GetItemProjection(InventoryItemView inventoryItem, Vector3 screenPos, out Vector3 projectionPos, out bool isPositionLegal, out ICommand placeItemCommand)
        {
            if(!RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPos, _camera, out var localPoint))
                throw new Exception("Screen pos to local in rect failed");
            
            if(!LocalRectPosToCell(localPoint, out var cellColumn, out var cellRow))
                throw new Exception("Local rect pos to cell failed");
            
            var halfCell = Vector2.one * CellSize / 2f;

            var corner1LocalPos = localPoint - (inventoryItem.Size / 2f - halfCell);
            var corner2LocalPos = localPoint + (inventoryItem.Size / 2f - halfCell);

            isPositionLegal =
                LocalRectPosToCell(corner1LocalPos, out var column1, out var row1) && 
                LocalRectPosToCell(corner2LocalPos, out var column2, out var row2) && 
                _gridSpace.IntersectsWithOneOrLessItems(
                    new GridSpaceItem(inventoryItem.Id, inventoryItem.SizeInCells, new Vector2Int(column1, row1)),
                    out var replacedItemId);

            if (!isPositionLegal) 
                RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, screenPos, _camera, out projectionPos);
            else
                projectionPos = _rectTransform.TransformPoint(Vector2.Lerp(corner1LocalPos, corner2LocalPos, .5f));
            
            _lastInsertItemCommand?.Release();
            _lastInsertItemCommand = InsertItemCommandsPool.Get();
            _lastInsertItemCommand.Value.Item.Setup(this, inventoryItem, new GridSpaceItem(inventoryItem.Id, inventoryItem.SizeInCells, new Vector2Int(column1, row1)));
            placeItemCommand = _lastInsertItemCommand.Value.Item;
        }

        private void ExtractItem(InventoryItemView inventoryItem)
        {
            if(!_gridSpace.Remove(inventoryItem.Id))
                throw new InvalidOperationException();
            
            if(!_items.Remove(inventoryItem))
                throw new InvalidOperationException();
            
            inventoryItem.SetParent(null);
        }

        private void InsertItem(InventoryItemView inventoryItem, GridSpaceItem gridInfo)
        {
            if(!_items.Add(inventoryItem))
                throw new InvalidOperationException();
            _gridSpace.Insert(gridInfo);
            inventoryItem.SetParent(transform);
        }

        private class ExtractItemCommand : ICommand, Libs_Pools_IPoolable
        {
            private GridSpaceItemContainer _container;
            private InventoryItemView _inventoryItem;
            private GridSpaceItem _gridItem;
            int IPoolable.RecyclesCount { get; set; }
            
            public void Setup(GridSpaceItemContainer container, InventoryItemView inventoryItem, GridSpaceItem gridItem)
            {
                _container = container;
                _inventoryItem = inventoryItem;
                _gridItem = gridItem;
            }

            public void Execute() => 
                _container.ExtractItem(_inventoryItem);

            public void Undo() => 
                _container.InsertItem(_inventoryItem, _gridItem);

            void IPoolable.Reset()
            {
                _inventoryItem = null;
                _gridItem = default;
                _container = null;
            }
        }
        
        private class InsertItemCommand : ICommand, Libs_Pools_IPoolable
        {
            private GridSpaceItemContainer _container;
            private InventoryItemView _inventoryItem;
            private GridSpaceItem _gridItem;
            
            int IPoolable.RecyclesCount { get; set; }
            
            public void Setup(GridSpaceItemContainer container, InventoryItemView inventoryItem, GridSpaceItem gridItem)
            {
                _container = container;
                _inventoryItem = inventoryItem;
                _gridItem = gridItem;
            }

            public void Execute() => 
                _container.InsertItem(_inventoryItem, _gridItem);

            public void Undo() => 
                throw new NotImplementedException();

            void IPoolable.Reset()
            {
                _inventoryItem = null;
                _gridItem = default;
                _container = null;
            }
        }
    }
}