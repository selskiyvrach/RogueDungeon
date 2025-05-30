using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Game.Features.Levels.Domain
{
    public class Room : IRoom
    {
        private readonly IFactory<RoomEventConfig, IRoomEvent> _roomEventFactory;
        private readonly RoomConfig _config;
        private readonly SortedList<RoomEventPriority, IRoomEvent> _events = new();
        private IRoomEvent _currentEvent;

        public Vector2Int Coordinates => _config.Coordinates;
        public AdjacentRooms AdjacentRooms { get; set; }
        public bool CanLeave => _events.Count == 0;
        public RoomGameObject GameObject { get; }

        public Room(RoomConfig config, IFactory<RoomEventConfig, IRoomEvent> roomEventFactory, RoomGameObject gameObject)
        {
            _config = config;
            _roomEventFactory = roomEventFactory;
            GameObject = gameObject;
            GameObject.transform.localPosition = new Vector3(Coordinates.x, 0, Coordinates.y);
        }

        public void Initialize() => 
            _config.EventConfigs.ForEach(n => AddEvent(_roomEventFactory.Create(n)));

        public void Enter() => 
            PickNextEvent();

        public void Exit() => 
            FinishEvent();

        public void AddEvent(IRoomEvent roomEvent) => 
            _events.Add(roomEvent.Priority, roomEvent);

        public void Tick(float timeDelta)
        {
            if(_currentEvent == null)
                return;
            
            _currentEvent.Tick(timeDelta);
            if (!_currentEvent.IsFinished) 
                return;
            
            FinishEvent();
            PickNextEvent();
        }

        private void PickNextEvent()
        {
            if (!_events.Any())
                return;

            _currentEvent = _events.Last().Value;
            _currentEvent.Room = this;
            _currentEvent.Enter();
            _events.RemoveAt(_events.Count - 1);
        }

        private void FinishEvent()
        {
            _currentEvent?.Exit();
            _currentEvent = null;
        }
    }
}