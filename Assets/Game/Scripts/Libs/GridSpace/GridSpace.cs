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
        }

        public bool IntersectsWithOneOrLessItems(GridSpaceItem item, out string intersectedItemId)
        {
            intersectedItemId = null;
            foreach (var cell in GetOccupiedCells(item))
            {
                if(_occupiedCells[cell.x, cell.y] == null)
                    continue;

                if (intersectedItemId == null)
                    intersectedItemId = _occupiedCells[cell.x, cell.y];
                else if(intersectedItemId != _occupiedCells[cell.x, cell.y])
                    return false;
            }
            return true;
        }

        private IEnumerable<Vector2Int> GetOccupiedCells(GridSpaceItem item)
        {
            for (var i = 0; i < item.Size.x; i++)
            for (var j = 0; j < item.Size.y; j++)
                yield return new Vector2Int(item.Position.x + i, item.Position.y + j);
        }

        public void Insert(GridSpaceItem item)
        {
            if (!_items.Add(item))
                throw new InvalidOperationException();
            
            foreach (var cell in GetOccupiedCells(item))
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
            var itemToRemove = _items.FirstOrDefault(n => n.Id == id);
            if(!_items.Remove(itemToRemove))
                return false;
            
            foreach (var cell in GetOccupiedCells(itemToRemove)) 
                _occupiedCells[cell.x, cell.y] = null;
            
            return true;
        }
    }
}