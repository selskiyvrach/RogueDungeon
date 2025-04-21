using System;
using System.Collections.Generic;
using System.Linq;
using Common.Lifecycle;
using Common.Unity;

namespace RogueDungeon.Levels
{
    public class Level : ITickable
    {
        private readonly Room[] _rooms;
        private ITwoDWorldObject _levelTraverser;
        public IRoom StartingRoom { get; }
        public IRoom CurrentRoom { get; private set; }
        public event Action OnChangedRoom;
        public IEnumerable<IRoom> Rooms => _rooms;

        public ITwoDWorldObject LevelTraverser
        {
            get => _levelTraverser;
            set
            {
                _levelTraverser = value;
                RefreshCurrentRoom();
            }
        }

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

        public void Tick(float timeDelta) => 
            CurrentRoom?.Tick(timeDelta);

        public void RefreshCurrentRoom()
        {
            var newRoom = Rooms.FirstOrDefault(n => n.Coordinates == LevelTraverser.LocalPosition.Round());
            if(newRoom == CurrentRoom)
                return;
            CurrentRoom = newRoom;
            OnChangedRoom?.Invoke();
        }
    }
}