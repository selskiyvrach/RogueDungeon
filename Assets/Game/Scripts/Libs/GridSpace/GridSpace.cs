using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Libs.GridSpace
{
    public class GridSpace
    {
        private readonly string[,] _occupiedCells;
        private readonly HashSet<GridSpaceItem> _items = new(20);
        private readonly int _rows;
        private readonly int _columns;

        public Vector2Int Size => new(_columns, _rows);

        public GridSpace(int columns, int rows)
        {
            _columns = columns;
            _rows = rows;
            _occupiedCells = new string[columns, rows];
            for (var i = 0; i < _occupiedCells.GetLength(0); i++)
            for (var j = 0; j < _occupiedCells.GetLength(1); j++)
                _occupiedCells[i, j] = null;
        }

        public bool ContainedInSpace(GridSpaceItem item) => 
            item.Position.x + item.Size.x <= _columns && item.Position.y + item.Size.y <= _rows;
        
        public bool ContainedInSpace(int column, int row) => 
            column < _columns && row < _rows;

        public bool HasItem(int column, int row, out GridSpaceItem item)
        {
            item = default;
            if(_occupiedCells[column, row] == null)
                return false;
            item = GetItem(_occupiedCells[column, row]);
            return true;
        }

        public bool IntersectsWithOneOrLessItems(GridSpaceItem item, out string intersectedItemId)
        {
            intersectedItemId = null;
            foreach (var cell in item.CoveredCells)
            {
                if(_occupiedCells[cell.x, cell.y] == null)
                    continue;

                if (intersectedItemId == null)
                    intersectedItemId = _occupiedCells[cell.x, cell.y];
                else
                    return false;
            }
            return true;
        }

        public void Insert(GridSpaceItem item)
        {
            if (!_items.Add(item))
                throw new InvalidOperationException();
            
            foreach (var cell in item.CoveredCells)
            {
                if(_occupiedCells[cell.x, cell.y] != null)
                    throw new InvalidOperationException();
                _occupiedCells[cell.x, cell.y] = item.Id;
            }
        }

        public GridSpaceItem GetItem(string id) => 
            _items.FirstOrDefault(i => i.Id == id);

        public bool Remove(string id)
        {
            var itemToRemove = (GridSpaceItem)default;
            foreach (var item in _items)
            {
                if(item.Id != id)
                    continue;
                itemToRemove = item;
                break;
            }
            if(!_items.Remove(itemToRemove))
                return false;
            
            foreach (var cell in itemToRemove.CoveredCells) 
                _occupiedCells[cell.x, cell.y] = null;
            
            return true;
        }
    }
}