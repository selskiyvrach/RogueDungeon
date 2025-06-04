using System.Collections.Generic;
using Game.Libs.WorldObjects;
using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public class Level
    {
        private readonly Dictionary<Vector2Int, Room> _rooms;

        private ITwoDWorldObject _levelTraverser;
        public Room StartingRoom { get; }
        public IEnumerable<Room> AllRooms => _rooms.Values;

        public Level(Room startingRoom, Dictionary<Vector2Int, Room> rooms)
        {
            StartingRoom = startingRoom;
            _rooms = rooms;
        }

        public Room GetRoom(Vector2Int position) => 
            _rooms[position];
    }
}