using System.Collections.Generic;
using System.Linq;
using Common.UtilsDotNet;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Player.Model.Inventory
{
    public class GridSpace
    {
        private readonly Vector2Int _size;
        private readonly int[,] _occupiedCells;
        private readonly List<GridSpaceItem> _items = new(20);

        public GridSpace()
        {
            var size = new Vector2Int(10, 5);
            _size = size;
            _occupiedCells = new int[size.x, size.y];
            for (var i = 0; i < _occupiedCells.GetLength(0); i++)
            for (var j = 0; j < _occupiedCells.GetLength(1); j++)
                _occupiedCells[i, j] = -1;
        }
        
        public bool ContainedInSpace(GridSpaceItem item) => 
            item.Position.x + item.Size.x <= _size.x && item.Position.y + item.Size.y <= _size.y;

        public bool IntersectsWithNoMoreThanOneItem(GridSpaceItem item, out int intersectedItemId)
        {
            intersectedItemId = -1;
            foreach (var cell in item.CoveredCells)
            {
                if(_occupiedCells[cell.x, cell.y] == -1)
                    continue;

                if (intersectedItemId == -1)
                    intersectedItemId = _occupiedCells[cell.x, cell.y];
                else
                    return false;
            }
            return true;
        }

        public void Place(GridSpaceItem item)
        {
            Assert.IsTrue(IntersectsWithNoMoreThanOneItem(item, out int _));
            Assert.IsFalse(_items.Any(n => n.Id == item.Id));
            
            _items.Add(item);
            foreach (var cell in item.CoveredCells) 
                _occupiedCells[cell.x, cell.y] = item.Id;
        }
        
        public void Remove(int id)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if(_items[i].Id != id)
                    continue;
                foreach (var cell in _items[i].CoveredCells) 
                    _occupiedCells[cell.x, cell.y] = -1;    
                _items.RemoveAt(i);
                break;
            }
            Debug.LogError($"Item with id '{id}' not found");
        }
    }
}