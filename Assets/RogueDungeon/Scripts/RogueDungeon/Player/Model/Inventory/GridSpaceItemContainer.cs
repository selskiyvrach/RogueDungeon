using System;
using System.Collections.Generic;
using System.Linq;
using Common.Pools;
using Common.UtilsDotNet;
using RogueDungeon.Camera;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IPoolable = Common.Pools.IPoolable;

namespace RogueDungeon.Player.Model.Inventory
{
    public class GridSpaceItemContainer : ItemContainer
    {
        [SerializeField] private GridLayoutGroup _gridLayout;
        
        private static readonly Pool<ExtractItemCommand> ExtractItemCommandsPool = new(() => new ExtractItemCommand(), 1);
        private static readonly Pool<InsertItemCommand> InsertItemCommandsPool = new(() => new InsertItemCommand(), 1);

        private readonly HashSet<ItemView> _items = new();

        private Poolable<ExtractItemCommand>? _lastExtractItemCommand;
        private Poolable<InsertItemCommand>? _lastInsertItemCommand;
        
        private GridSpace _gridSpace;
        private IGameCamera _camera;
        private int _rows;
        private int _columns;
        protected override float CellSize => _gridLayout.cellSize.x;

        [Inject]
        public void Construct(IGameCamera camera)
        {
            _camera = camera;
            _gridLayout.GetRowsAndColumns(out _rows, out _columns);
            _gridSpace = new GridSpace(_columns, _rows);
        }
        
        protected override ItemView RaycastItem(Vector3 screenPos, out ICommand extractItemCommand)
        {
            extractItemCommand = null;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPos, _camera.Camera,  out var localPoint);
            LocalRectPosToCell(localPoint, out var cellColumn, out var cellRow);

            if(!_gridSpace.HasItem(cellColumn, cellRow, out var item))
                return null;

            var itemView = _items.First(n => n.InstanceId == item.Id);
            
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

        protected override void GetItemProjection(ItemView item, Vector3 screenPos, out Vector3 projectionPos, out bool isPositionLegal, out ICommand placeItemCommand)
        {
            if(!RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPos, _camera.Camera, out var localPoint))
                throw new Exception("Screen pos to local in rect failed");
            
            if(!LocalRectPosToCell(localPoint, out var cellColumn, out var cellRow))
                throw new Exception("Local rect pos to cell failed");
            
            var halfCell = Vector2.one * CellSize / 2f;

            var corner1LocalPos = localPoint - (item.Size / 2f - halfCell);
            var corner2LocalPos = localPoint + (item.Size / 2f - halfCell);

            isPositionLegal =
                LocalRectPosToCell(corner1LocalPos, out var column1, out var row1) && 
                LocalRectPosToCell(corner2LocalPos, out var column2, out var row2) && 
                _gridSpace.IntersectsWithOneOrLessItems(
                    new GridSpaceItem(item.InstanceId, item.SizeInCells, new Vector2Int(column1, row1)),
                    out var replacedItemId);

            if (!isPositionLegal) 
                RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, screenPos, _camera.Camera, out projectionPos);
            else
                projectionPos = _rectTransform.TransformPoint(Vector2.Lerp(corner1LocalPos, corner2LocalPos, .5f));
            
            _lastInsertItemCommand?.Release();
            _lastInsertItemCommand = InsertItemCommandsPool.Get();
            _lastInsertItemCommand.Value.Item.Setup(this, item, new GridSpaceItem(item.InstanceId, item.SizeInCells, new Vector2Int(column1, row1)));
            placeItemCommand = _lastInsertItemCommand.Value.Item;
        }

        private void ExtractItem(ItemView item)
        {
            if(!_gridSpace.Remove(item.InstanceId))
                throw new InvalidOperationException();
            
            if(!_items.Remove(item))
                throw new InvalidOperationException();
            
            item.SetParent(null);
        }

        private void InsertItem(ItemView item, GridSpaceItem gridInfo)
        {
            if(!_items.Add(item))
                throw new InvalidOperationException();
            _gridSpace.Insert(gridInfo);
            item.SetParent(transform);
        }

        private class ExtractItemCommand : ICommand, IPoolable
        {
            private GridSpaceItemContainer _container;
            private ItemView _item;
            private GridSpaceItem _gridItem;
            int IPoolable.RecyclesCount { get; set; }
            
            public void Setup(GridSpaceItemContainer container, ItemView item, GridSpaceItem gridItem)
            {
                _container = container;
                _item = item;
                _gridItem = gridItem;
            }

            public void Execute() => 
                _container.ExtractItem(_item);

            public void Undo() => 
                _container.InsertItem(_item, _gridItem);

            void IPoolable.Reset()
            {
                _item = null;
                _gridItem = default;
                _container = null;
            }
        }
        
        private class InsertItemCommand : ICommand, IPoolable
        {
            private GridSpaceItemContainer _container;
            private ItemView _item;
            private GridSpaceItem _gridItem;
            
            int IPoolable.RecyclesCount { get; set; }
            
            public void Setup(GridSpaceItemContainer container, ItemView item, GridSpaceItem gridItem)
            {
                _container = container;
                _item = item;
                _gridItem = gridItem;
            }

            public void Execute() => 
                _container.InsertItem(_item, _gridItem);

            public void Undo() => 
                throw new NotImplementedException();

            void IPoolable.Reset()
            {
                _item = null;
                _gridItem = default;
                _container = null;
            }
        }
    }
}