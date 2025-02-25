using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Levels
{
    public class AdjacentRooms
    {
        private static readonly Vector2Int[] ValidAdjacencies = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, };
        private readonly IRoom _thisRoom;
        private readonly Dictionary<Vector2Int, IRoom> _adjacentRooms;

        public AdjacentRooms(IRoom thisRoom, IEnumerable<IRoom> adjacentRooms)
        {
            _thisRoom = thisRoom;
            _adjacentRooms = adjacentRooms.ToDictionary(n => n.Coordinates - _thisRoom.Coordinates, n => n);
            Assert.IsTrue(_adjacentRooms.Count is > 0 and < 5);
            Assert.IsTrue(_adjacentRooms.All(n => ValidAdjacencies.Any(m => n.Key == m)));
        }

        public bool HasAdjacentRoom(Vector2Int adjacency) =>
            _adjacentRooms.ContainsKey(adjacency);

        public IRoom GetAdjacentRoom(Vector2Int adjacency) => 
            _adjacentRooms.GetValueOrDefault(adjacency);
    }
}