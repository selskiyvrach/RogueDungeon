using System;
using System.Collections.Generic;
using System.Linq;
using Game.Libs.WorldObjects;
using Libs.Lifecycle;
using Libs.Utils.Unity;

namespace Game.Features.Levels.Domain
{
    public class Level : ITickable
    {
        private readonly Room[] _rooms;
        private ITwoDWorldObject _levelTraverser;
        public Room StartingRoom { get; }
        public Room CurrentRoom { get; private set; }
        public event Action OnChangedRoom;
        public IEnumerable<Room> Rooms => _rooms;

        public ITwoDWorldObject LevelTraverser
        {
            get => _levelTraverser;
            set
            {
                _levelTraverser = value;
                RefreshCurrentRoom();
            }
        }

        public Level(Room startingRoom, IEnumerable<Room> rooms)
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