using System.Collections.Generic;

namespace RogueDungeon.Levels
{
    public class Level
    {
        public IRoom StartingRoom { get; }
        public IEnumerable<IRoom> Rooms { get; }

        public Level(IRoom startingRoom, IEnumerable<IRoom> rooms)
        {
            StartingRoom = startingRoom;
            Rooms = rooms;
        }
    }
}