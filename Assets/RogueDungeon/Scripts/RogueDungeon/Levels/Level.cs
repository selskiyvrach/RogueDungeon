using System.Collections.Generic;
using System.Linq;

namespace RogueDungeon.Levels
{
    public class Level
    {
        private readonly Room[] _rooms;
        public IRoom StartingRoom { get; }
        public IEnumerable<IRoom> Rooms => _rooms;
        public ILevelTraverser LevelTraverser { get; set; }

        public Level(IRoom startingRoom, IEnumerable<Room> rooms)
        {
            StartingRoom = startingRoom;
            _rooms = rooms.ToArray();
        }

        public void Initialize()
        {
            foreach (var room in _rooms) 
                room.Initialize();
        }
    }
}