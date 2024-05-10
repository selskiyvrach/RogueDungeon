using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RogueDungeon.Maze
{
    public class Maze
    {
        private readonly Dictionary<Vector2Int, Tile> _tiles;
        private readonly Vector2Int[] _directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left, };

        public Maze(IEnumerable<Tile> tiles) => 
            _tiles = tiles.ToDictionary(n => n.Coordinates, n => n);

        public bool IsCrossroad(Vector2Int pos)
        {
            var count = 0;
            var firstWay = Vector2Int.zero;
            
            foreach (var way in GetWaysFromTile(pos))
            {
                if (count++ == 0)
                    firstWay = way;
                
                if (count == 2 && firstWay + way != Vector2Int.zero)
                    return true;
                
                if (count == 3)
                    return true;
            }

            return false;
        }

        public Vector2Int TurnAround(Vector2Int initial) =>
            initial.x != 0
                ? new Vector2Int(initial.x * -1, initial.y)
                : new Vector2Int(initial.x, initial.y * -1);
        
        public Vector2Int TurnClockwise(Vector2Int initial)
        {
            for (var i = 0; i < _directions.Length; i++)
            {
                if (_directions[i] == initial)
                    return _directions[(i + 1) % _directions.Length];
            }

            throw new InvalidOperationException();
        }
        
        public Vector2Int TurnCounterclockwise(Vector2Int initial)
        {
            for (var i = 0; i < _directions.Length; i++)
            {
                if (_directions[i] == initial)
                    return _directions[i == 0 ? 3 : i - 1];
            }

            throw new InvalidOperationException();
        }

        public IEnumerable<Vector2Int> GetWaysFromTile(Vector2Int pos) => 
            from direction in _directions where HasTile(pos + direction) select direction;

        public bool HasTile(Vector2Int pos) => 
            _tiles.ContainsKey(pos);

        public Tile GetTile(Vector2Int pos) => 
            _tiles[pos];
    }
}